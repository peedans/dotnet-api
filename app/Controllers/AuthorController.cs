using dotnetapi.Services.Master;
using dotnetapi.ViewModels.Base;
using dotnetapi.ViewModels.Master;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapi.Controllers;

    [Route("api/author")]
    [ApiController]
    [Produces("application/json")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet]
        public IActionResult GetAuthor()
        {
            var result = this.authorService.GetAll();
            return Ok(new BaseResponseView<List<AuthorResponseViewModel>>() { data = result });
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorById(long id)
        {
            var result = this.authorService.GetById(id);
            return Ok(new BaseResponseView<AuthorResponseViewModel>() { data = result });
        }

        [HttpPost("")]
        public IActionResult CreateAuthor([FromBody] AuthorRequestViewModel author)
        {
            var result = this.authorService.Create(author);
            return Ok(new BaseResponseView<AuthorResponseViewModel>() { data = result });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(long id, AuthorRequestViewModel author)
        {
            var result = this.authorService.Update(id, author);

            return Ok(new BaseResponseView<bool>() { data = result });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(long id)
        {
            var result = this.authorService.Delete(id);

            return Ok(new BaseResponseView<bool>() { data = result });
        }

        [HttpPost("query")]
        public IActionResult QueryAuthors([FromBody] AuthorRequestPageViewModel payload)
        {
            var result = this.authorService.Query(payload);
            return Ok(new BaseResponsePageView<List<AuthorResponseViewModel>>() { data = result });
        }
    }
