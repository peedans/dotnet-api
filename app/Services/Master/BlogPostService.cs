using dotnetapi.ViewModels.Master;
using AutoMapper;
using dotnetapi.Models.Master;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using dotnetapi.ViewModels.Base;
using dotnetapi.Utils;
using NTR.Common.DataAccess;
using app.ViewModels.Master;
using app.Models.Master;
using NTR.Common.DataAccess.Repositories;

namespace dotnetapi.Services.Master
{
    public interface IBlogPostService
    {
        List<BlogPostResponseViewModel> GetAll();
        BlogPostResponseViewModel GetById(long id);
        BlogPostResponseViewModel Create(BlogPostRequestViewModel blog);
        BaseResponsePageDataModel<List<BlogPostResponseViewModel>> Query(BlogPostRequestPageViewModel payload);
        bool Update(long id, BlogPostRequestUpdateViewModel blog);
        bool Delete(long id);
        bool ActiveBlogPostCategory(BlogPostCategoryRequestActiveViewModel payload);
        bool InActiveBlogPostCategory(BlogPostCategoryRequestActiveViewModel payload);
    }

    public class BlogPostService : IBlogPostService
    {
        private readonly IUowProvider uowProvider;
        private readonly IMapper mapper;
        private readonly ISearchUtil searchUtil;

        public BlogPostService(IMapper mapper, ISearchUtil searchUtil, IUowProvider uowProvider)
        {
            this.mapper = mapper;
            this.searchUtil = searchUtil;
            this.uowProvider = uowProvider;
        }

        public List<BlogPostResponseViewModel> GetAll()
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<BlogPost>();
                var blogPosts = repository.Filters(t => t.is_deleted == false)
                      .Include(x => x.Author)
                      .Include(x => x.BlogPostTags!)
                      .ThenInclude(x => x.Tag)
                      .Include(x => x.BlogPostCategory!.Where(c => c.IsActive == true))
                      .ThenInclude(x => x.Category)
                      .AsNoTracking()
                      .ToList();
                return this.mapper.Map<List<BlogPostResponseViewModel>>(blogPosts);
            }
        }

        public BlogPostResponseViewModel GetById(long id)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<BlogPost>();
                var blogPost = repository.Filters(x => x.id == id && x.is_deleted == false)
                      .Include(x => x.Author)
                      .Include(x => x.BlogPostTags!)
                      .ThenInclude(x => x.Tag)
                      .Include(x => x.BlogPostCategory!.Where(c => c.IsActive == true))
                      .ThenInclude(x => x.Category)
                      .AsNoTracking()
                      .FirstOrDefault();

                return this.mapper.Map<BlogPostResponseViewModel>(blogPost);
            }
        }

        public BlogPostResponseViewModel Create(BlogPostRequestViewModel blog)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repositoryAuthor = uow.GetRepository<Author>();
                var author = repositoryAuthor.Filters(a => a.id == blog.author_id && a.is_deleted == false).FirstOrDefault();

                if (author == null)
                {
                    throw new Exception("author not found");
                }
                var blogPost = this.mapper.Map<BlogPost>(blog);

                var repositoryTag = uow.GetRepository<Tag>();
                var tags = new List<Tag>();
                foreach (var tag in blog.tags!)
                {
                    var exitsTag = repositoryTag.Filters(x => x.Name!.ToLower() == tag.name!.ToLower()).FirstOrDefault();
                    if (exitsTag == null)
                    {
                        var newTag = this.mapper.Map<Tag>(tag);
                        tags.Add(newTag);
                    }
                    else
                    {
                        tags.Add(exitsTag);
                    }
                }

                blogPost.BlogPostTags = tags.Select(t => new BlogPostTag
                {
                    Tag = t,
                    BlogPost = blogPost
                }).ToList();

                var repositoryBlogPost = uow.GetRepository<BlogPost>();
                repositoryBlogPost.Add(blogPost);
                uow.SaveChanges();

                return this.mapper.Map<BlogPostResponseViewModel>(blogPost);
            }
        }

        public BaseResponsePageDataModel<List<BlogPostResponseViewModel>> Query(BlogPostRequestPageViewModel payload)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<BlogPost>();
                var blogPosts = repository.Filters(x => x.is_deleted == false)
                        .Include(x => x.Author)
                        .Include(x => x.BlogPostTags!)
                        .ThenInclude(x => x.Tag)
                        .AsNoTracking()
                        .ToList();
                var lists = mapper.Map<List<BlogPostResponseViewModel>>(blogPosts).AsQueryable();

                if (payload.search_text != null && payload.search_text!.fields!.Any() && !string.IsNullOrEmpty(payload.search_text?.input_text))
                {
                    var searchBlogPosts = searchUtil.SearchText(lists, payload.search_text.fields, payload.search_text.input_text);
                    lists = mapper.Map<List<BlogPostResponseViewModel>>(searchBlogPosts.Distinct()).AsQueryable();
                }

                if (payload.filter != null)
                {
                    var query = searchUtil.FilterQuery(payload.filter);
                    if (!string.IsNullOrEmpty(query))
                    {
                        lists = lists.Where(query);
                    }
                }

                lists = lists.OrderBy(string.Format("{0} {1}", payload.order_by, payload.is_order_reverse ? "desc" : "asc"));
                payload.item_total = lists.Count();
                var resultPages = lists.Page(payload.page_index > 0 ? payload.page_index : 1, payload.item_per_page).ToList();

                return new BaseResponsePageDataModel<List<BlogPostResponseViewModel>>()
                {
                    list = resultPages,
                    paging = payload
                };
            }
        }

        public bool Update(long id, BlogPostRequestUpdateViewModel blog)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<BlogPost>();
                var existingBlogPost = repository.Filters(x => x.id == id && x.is_deleted == false).FirstOrDefault();

                if (existingBlogPost == null)
                    throw new Exception("Blog not found");

                existingBlogPost = mapper.Map<BlogPostRequestUpdateViewModel, BlogPost>(blog, existingBlogPost);
                uow.SaveChanges();

                return true;
            }
        }

        public bool Delete(long id)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<BlogPost>();
                var blogPost = repository.Filters(a => a.id == id && a.is_deleted == false).FirstOrDefault();

                if (blogPost == null)
                {
                    throw new Exception("BlogPost not found");
                }

                blogPost.is_deleted = true;
                uow.SaveChanges();

                return true;
            }
        }

        private List<Tag> CreateTags(List<TagRequestViewModel> tagNames)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var newTags = new List<Tag>();
                var repository = uow.GetRepository<Tag>();
                if (tagNames != null)
                {
                    foreach (var tag in tagNames)
                    {
                        var exitsTag = repository.Filters(x => x.Name!.ToLower() == tag.name!.ToLower()).FirstOrDefault();
                        if (exitsTag == null)
                        {
                            var newTag = this.mapper.Map<Tag>(tag);
                            repository.Add(newTag);
                            newTags.Add(newTag);
                        }
                        else
                        {
                            newTags.Add(exitsTag);
                        }
                    }
                }

                return newTags;
            }

        }

        public bool ActiveBlogPostCategory(BlogPostCategoryRequestActiveViewModel payload)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                return ValidateBlogPostCategory(uow, payload, true);
            }

        }

        public bool InActiveBlogPostCategory(BlogPostCategoryRequestActiveViewModel payload)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                return ValidateBlogPostCategory(uow, payload, false);
            }
        }

        private bool ValidateBlogPostCategory(IUnitOfWork uow, BlogPostCategoryRequestActiveViewModel payload, bool isActive)
        {
            var repository = uow.GetRepository<BlogPostCategory>();
            var BlogPostRepository = uow.GetRepository<BlogPost>();
            var blogpostExists = BlogPostRepository.Any(a => a.id == payload.blogpost_id && a.is_deleted == false);
            if (!blogpostExists)
            {
                throw new Exception("BlogPost ID not found.");
            }

            var CategoryRepository = uow.GetRepository<Category>();
            var categoryExists = CategoryRepository.Any(a => a.id == payload.category_id && a.is_deleted == false);
            if (!categoryExists)
            {
                throw new Exception("Category ID not found.");
            }

            var BlogPostCategoryExists = repository.Filters(uc => uc.BlogPostId == payload.blogpost_id && uc.CategoryId == payload.category_id).FirstOrDefault();
            if (BlogPostCategoryExists == null)
            {
                BlogPostCategoryExists = this.mapper.Map<BlogPostCategory>(payload);
                repository.Add(BlogPostCategoryExists);
            }

            BlogPostCategoryExists.IsActive = isActive;

            uow.SaveChanges();

            return true;
        }
    }
}