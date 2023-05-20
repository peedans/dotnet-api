using dotnetapi.Services;
using dotnetapi.ViewModels.Base;
using Microsoft.AspNetCore.Mvc;
using dotnetapi.ViewModels.User;
using dotnetapi.Services.User;

namespace dotnetapi.Controllers
{

    [Route("api/user")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet("")]
        public IActionResult GetUsers()
        {
            var result = this.userService.GetAll();
            return Ok(new BaseResponseView<List<UserResponseViewModel>>() { data = result });
        }
        
        [HttpGet("{id}")]
        public IActionResult GetUserById(long id)
        {
            var result = this.userService.GetById(id);
            return Ok(new BaseResponseView<UserResponseViewModel>() { data = result });
        }

         [HttpPost("")]
        public IActionResult CreateUser([FromBody] UserRequestViewModel User)
        {
            var result = this.userService.Create(User);
            return Ok(new BaseResponseView<UserResponseViewModel>() { data = result });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(long id, UserRequestUpdateViewModel User)
        {
            var result = this.userService.Update(id, User);

            return Ok(new BaseResponseView<bool>() { data = result });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(long id)
        {
            var result = this.userService.Delete(id);

            return Ok(new BaseResponseView<bool>() { data = result });
        }

         [HttpPost("usercompany")]
        public IActionResult CreateUserCompany([FromBody] UserCompanyRequestViewModel userCompany)
        {
            var result = this.userService.CreateUserCompany(userCompany);
            return Ok(new BaseResponseView<UserCompanyResponseViewModel>() { data = result });
        }

        [HttpPut("userCompany/switchCompany")]
        public IActionResult SwitchUserCompany( UserCompanyRequestUpdateViewModel payload)
        {
            var result = this.userService.SwitchUserCompany( payload);

            return Ok(new BaseResponseView<bool>() { data = result });
        }

         [HttpDelete("usercompany")]
        public IActionResult DeleteUserCompany(UserCompanyDeleteRequestViewModel userCompany)
        {
            var result = this.userService.DeleteUserCompany(userCompany);

            return Ok(new BaseResponseView<bool>() { data = result });
        }
    }
}