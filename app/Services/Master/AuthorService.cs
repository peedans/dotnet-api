using dotnetapi.ViewModels.Master;
using AutoMapper;
using dotnetapi.Models.Master;
using Microsoft.EntityFrameworkCore;
using dotnetapi.ViewModels.Base;
using System.Linq.Dynamic.Core;
using dotnetapi.Utils;
using NTR.Common.DataAccess;
namespace dotnetapi.Services.Master
{
    public interface IAuthorService
    {
        List<AuthorResponseViewModel> GetAll();
        AuthorResponseViewModel GetById(long id);
        AuthorResponseViewModel Create(AuthorRequestViewModel author);
        BaseResponsePageDataModel<List<AuthorResponseViewModel>> Query(AuthorRequestPageViewModel payload);
        bool Update(long id, AuthorRequestViewModel author);
        bool Delete(long id);
    }

    public class AuthorService : IAuthorService
    {
        private readonly IUowProvider uowProvider;
        private readonly IMapper mapper;
        private readonly ISearchUtil searchUtil;

        public AuthorService(DBMasterContext dbContext, IMapper mapper, ISearchUtil searchUtil, IUowProvider uowProvider)
        {
            this.mapper = mapper;
            this.searchUtil = searchUtil;
            this.uowProvider = uowProvider;
        }

        public List<AuthorResponseViewModel> GetAll()
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Author>();
                var authors = repository.GetAllActive();

                return this.mapper.Map<List<AuthorResponseViewModel>>(authors);
            }
        }

        public AuthorResponseViewModel GetById(long id)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Author>();
                var author = repository.GetActive(id);

                if (author == null)
                    throw new Exception("Author not found");

                return this.mapper.Map<AuthorResponseViewModel>(author);
            }
        }

        public AuthorResponseViewModel Create(AuthorRequestViewModel author)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Author>();

                if (repository.Any(a => a.EmailAddress == author.email_address && a.is_deleted == false))
                    throw new Exception("Email already exists");

                var newAuthor = this.mapper.Map<Author>(author);
                repository.Add(newAuthor);
                uow.SaveChanges();

                return this.mapper.Map<AuthorResponseViewModel>(newAuthor);
            }
        }

        public BaseResponsePageDataModel<List<AuthorResponseViewModel>> Query(AuthorRequestPageViewModel payload)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Author>();
                var authors = repository.GetAllActive();
                var lists = mapper.Map<List<AuthorResponseViewModel>>(authors).AsQueryable();

                if (payload.search_text != null && payload.search_text!.fields!.Any() && !string.IsNullOrEmpty(payload.search_text?.input_text))
                {
                    var searchAuthors = searchUtil.SearchText(lists, payload.search_text.fields, payload.search_text.input_text);
                    lists = mapper.Map<List<AuthorResponseViewModel>>(searchAuthors.Distinct()).AsQueryable();
                }

                if (payload.filter != null)
                {
                    var query = "";
                    foreach (var property in payload.filter!.GetType().GetProperties())
                    {
                        if (string.IsNullOrEmpty($"{property.GetValue(payload.filter)}") || property.GetValue(payload.filter) == null)
                        {
                            continue;
                        }

                        if (property.Name != payload.filter!.GetType().GetProperties().First().Name && !string.IsNullOrEmpty(query))
                        {
                            query += "and ";
                        }

                        query += $"{property.Name} == \"{property.GetValue(payload.filter)}\" ";
                    }

                    if (!string.IsNullOrEmpty(query))
                    {
                        lists = lists.Where(query);
                    }
                }


                lists = lists.OrderBy(string.Format("{0} {1}", payload.order_by, payload.is_order_reverse ? "desc" : "asc"));
                payload.item_total = lists.Count();
                var resultPages = lists.Page(payload.page_index > 0 ? payload.page_index : 1, payload.item_per_page).ToList();

                return new BaseResponsePageDataModel<List<AuthorResponseViewModel>>()
                {
                    list = resultPages,
                    paging = payload
                };
            }
        }

        public bool Update(long id, AuthorRequestViewModel author)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Author>();
                var existingAuthor = repository.GetActive(id);

                if (existingAuthor == null)
                {
                    throw new Exception("Author not found");
                }

                if (author.email_address != existingAuthor.EmailAddress)
                {
                    var emailExist = repository.Any(a => a.EmailAddress == author.email_address);

                    if (emailExist)
                    {
                        throw new Exception("Email already exists");
                    }
                }
                existingAuthor = mapper.Map<AuthorRequestViewModel,Author>(author, existingAuthor);
                uow.SaveChanges();

                return true;
            }
        }

        public bool Delete(long id)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Author>();
                var existingAuthor = repository.GetActive(id);

                if (existingAuthor == null)
                    throw new Exception("Author not found");

                existingAuthor.is_deleted = true;
                uow.SaveChanges();

                return true;
            }
        }

    }
}