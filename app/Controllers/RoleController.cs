using dotnetapi.ViewModels.Base;
using Microsoft.AspNetCore.Mvc;
using dotnetapi.ViewModels.User;
using dotnetapi.Services.User;

namespace dotnetapi.Controllers
{

    [Route("api/role")]
    [ApiController]
    [Produces("application/json")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        [HttpGet("")]
        public IActionResult GetRoles()
        {
            var result = this.roleService.GetAll();
            return Ok(new BaseResponseView<List<RoleResponseViewModel>>() { data = result });
        }
        
        [HttpGet("{id}")]
        public IActionResult GetRoleById(long id)
        {
            var result = this.roleService.GetById(id);
            return Ok(new BaseResponseView<RoleResponseViewModel>() { data = result });
        }

         [HttpPost("")]
        public IActionResult CreateRole([FromBody] RoleRequestViewModel role)
        {
            var result = this.roleService.Create(role);
            return Ok(new BaseResponseView<RoleResponseViewModel>() { data = result });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRole(long id, RoleRequestViewModel role)
        {
            var result = this.roleService.Update(id, role);

            return Ok(new BaseResponseView<bool>() { data = result });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRole(long id)
        {
            var result = this.roleService.Delete(id);

            return Ok(new BaseResponseView<bool>() { data = result });
        }

        //RoleAction
        [HttpPost("roleAction")]
        public IActionResult CreateRoleAction([FromBody] RoleActionRequestViewModel roleAction)
        {
            var result = this.roleService.CreateRoleAction(roleAction);
            return Ok(new BaseResponseView<RoleActionResponseViewModel>() { data = result });
        }

        [HttpDelete("roleAction")]
        public IActionResult DeleteRoleAction(RoleActionRequestViewModel roleAction)
        {
            var result = this.roleService.DeleteRoleAction(roleAction);

            return Ok(new BaseResponseView<bool>() { data = result });
        }
    }
}