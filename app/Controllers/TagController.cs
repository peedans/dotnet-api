using dotnetapi.Services.Master;
using dotnetapi.ViewModels.Base;
using dotnetapi.ViewModels.Master;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapi.Controllers;

    [Route("api/tag")]
    [ApiController]
    [Produces("application/json")]
    public class TagController : ControllerBase
    {
        private readonly ITagService tagService;

        public TagController(ITagService tagService)
        {
            this.tagService = tagService;
        }
        [HttpGet("")]
        public IActionResult GetTags()
        {
            var result = this.tagService.GetAll();
            return Ok(new BaseResponseView<List<TagViewModel>>() { data = result });
        }
        
        [HttpGet("{id}")]
        public IActionResult GetTagById(long id)
        {
            var result = this.tagService.GetById(id);
            return Ok(new BaseResponseView<TagViewModel>() { data = result });
        }

        [HttpPost("")]
        public IActionResult CreateTag(TagRequestViewModel tag)
        {
            var result = this.tagService.Create(tag);
            return Ok(new BaseResponseView<TagResponseViewModel>() { data = result });
        }

        [HttpPut("")]
        public IActionResult UpdateTag(long id, TagRequestViewModel tag)
        {
            var result = this.tagService.Update(id, tag);
            return Ok(new BaseResponseView<bool>() { data = result });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTagById(long id)
        {
            var result = this.tagService.Delete(id);
            return Ok(new BaseResponseView<bool>() { data = result });
        }

        [HttpPost("query")]
        public IActionResult QueryTags([FromBody] TagRequestPageViewModel payload)
        {

            var result = this.tagService.Query(payload);
            return Ok(new BaseResponsePageView<List<TagViewModel>>() { data = result });
        }
    }
