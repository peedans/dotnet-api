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
    public interface ITagService
    {
        List<TagViewModel> GetAll();
        TagViewModel GetById(long id);
        TagResponseViewModel Create(TagRequestViewModel tag);
        BaseResponsePageDataModel<List<TagViewModel>> Query(TagRequestPageViewModel payload);
        bool Update(long id, TagRequestViewModel tag);
        bool Delete(long id);
    }

    public class TagService : ITagService
    {
        private readonly IUowProvider uowProvider;
        private readonly IMapper mapper;
        private readonly ISearchUtil searchUtil;

        public TagService(IMapper mapper, ISearchUtil searchUtil, IUowProvider uowProvider)
        {
            this.mapper = mapper;
            this.searchUtil = searchUtil;
            this.uowProvider = uowProvider;
        }

        public List<TagViewModel> GetAll()
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Tag>();
                var tags = repository.GetAllActive();

                return this.mapper.Map<List<TagViewModel>>(tags);
            }
        }

        public TagViewModel GetById(long id)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Tag>();
                var tag = repository.GetActive(id);

                if (tag == null)
                    throw new Exception("Tag not found");

                return this.mapper.Map<TagViewModel>(tag);
            }
        }

        public TagResponseViewModel Create(TagRequestViewModel tag)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Tag>();
                var newTag = this.mapper.Map<Tag>(tag);
                repository.Add(newTag);
                uow.SaveChanges();

                return this.mapper.Map<TagResponseViewModel>(newTag);
            }
        }

        public BaseResponsePageDataModel<List<TagViewModel>> Query(TagRequestPageViewModel payload)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Tag>();
                var tags = repository.GetAllActive();
                var listTagViewModel = mapper.Map<List<TagViewModel>>(tags).AsQueryable();

                if (payload.search_text!.fields!.Any() && !string.IsNullOrEmpty(payload.search_text?.input_text))
                {
                    var searchTags = searchUtil.SearchText(listTagViewModel, payload.search_text.fields, payload.search_text.input_text);
                    listTagViewModel = mapper.Map<List<TagViewModel>>(searchTags.Distinct()).AsQueryable();
                }
                
                listTagViewModel = listTagViewModel.OrderBy(string.Format("{0} {1}", payload.order_by, payload.is_order_reverse ? "desc" : "asc"));
                payload.item_total = listTagViewModel.Count();
                var resultPages = listTagViewModel.Page(payload.page_index > 0 ? payload.page_index : 1, payload.item_per_page).ToList();

                return new BaseResponsePageDataModel<List<TagViewModel>>()
                {
                    list = resultPages,
                    paging = payload
                };
            }
        }

        public bool Update(long id, TagRequestViewModel tag)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Tag>();
                var existingTag = repository.GetActive(id);
                
                if (existingTag == null)
                    throw new Exception("Tag not found");

                existingTag = mapper.Map<TagRequestViewModel,Tag>(tag, existingTag);
                uow.SaveChanges();

                return true;
            }
        }

        public bool Delete(long id)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Tag>();
                var existingTag = repository.GetActive(id);
                if (existingTag == null)
                    throw new Exception("Tag not found");

                existingTag.is_deleted = true;
                uow.SaveChanges();

                return true;
            }
        }
    }
}
