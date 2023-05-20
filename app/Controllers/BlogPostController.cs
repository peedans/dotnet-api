using app.ViewModels.Master;
using dotnetapi.Services.Master;
using dotnetapi.ViewModels.Base;
using dotnetapi.ViewModels.Master;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapi.Controllers;

    [Route("api/blogpost")]
    [ApiController]
    [Produces("application/json")]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService blogPostService;

        public BlogPostController(IBlogPostService blogPostService)
        {
            this.blogPostService = blogPostService;
        }

        [HttpGet("")]
        public IActionResult GetBlog()
        {
            var result = this.blogPostService.GetAll();
            return Ok(new BaseResponseView<List<BlogPostResponseViewModel>>() { data = result });
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogPostById(long id)
        {
            var result = this.blogPostService.GetById(id);
            return Ok(new BaseResponseView<BlogPostResponseViewModel>() { data = result });
        }

        [HttpPost("")]
        public IActionResult CreateBlog(BlogPostRequestViewModel blog)
        {
            var result = this.blogPostService.Create(blog);
            return Ok(new BaseResponseView<BlogPostResponseViewModel>() { data = result });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(long id, BlogPostRequestUpdateViewModel blogPost)
        {
            var result = this.blogPostService.Update(id, blogPost);

            return Ok(new BaseResponseView<bool>() { data = result });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(long id)
        {
            var result = this.blogPostService.Delete(id);

            return Ok(new BaseResponseView<bool>() { data = result });
        }

        [HttpPost("query")]
        public IActionResult QueryAuthors([FromBody] BlogPostRequestPageViewModel payload)
        {
            var result = this.blogPostService.Query(payload);
            return Ok(new BaseResponsePageView<List<BlogPostResponseViewModel>>() { data = result });
        }

        [HttpPost("activeblogpostcategory")]
        public IActionResult ActiveBlogPostCategory(BlogPostCategoryRequestActiveViewModel payload)
        {
            var result = this.blogPostService.ActiveBlogPostCategory(payload);

            return Ok(new BaseResponseView<bool>() { data = result });
        }

        [HttpPost("inactiveblogpostcategory")]
        public IActionResult InActiveBlogPostCategory(BlogPostCategoryRequestActiveViewModel payload)
        {
            var result = this.blogPostService.InActiveBlogPostCategory(payload);

            return Ok(new BaseResponseView<bool>() { data = result });
        }
    }
