
using dotnetapi.Services.User;
using dotnetapi.ViewModels.Base;
using dotnetapi.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapi.Controllers;

[Route("api/action")]
[ApiController]
[Produces("application/json")]
public class ActionController : ControllerBase
{
     private readonly IActionService actionService;

        public ActionController(IActionService actionService)
        {
            this.actionService = actionService;
        }

        [HttpGet]
        public IActionResult GetAction()
        {
            var result = this.actionService.GetAll();
            return Ok(new BaseResponseView<List<ActionResponseViewModel>>() { data = result });
        }

        [HttpGet("{id}")]
        public IActionResult GetActionById(long id)
        {
            var result = this.actionService.GetById(id);
            return Ok(new BaseResponseView<ActionResponseViewModel>() { data = result });
        }

         [HttpPost("")]
        public IActionResult CreateAction([FromBody] ActionRequestViewModel action)
        {
            var result = this.actionService.Create(action);
            return Ok(new BaseResponseView<ActionResponseViewModel>() { data = result });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAction(long id, ActionRequestViewModel action)
        {
            var result = this.actionService.Update(id, action);

            return Ok(new BaseResponseView<bool>() { data = result });
        }

         [HttpDelete("{id}")]
        public IActionResult DeleteAction(long id)
        {
            var result = this.actionService.Delete(id);

            return Ok(new BaseResponseView<bool>() { data = result });
        }


}
