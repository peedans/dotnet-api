
using dotnetapi.Services.User;
using dotnetapi.ViewModels.Base;
using dotnetapi.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapi.Controllers;

[Route("api/department")]
[ApiController]
[Produces("application/json")]
public class DepartmentController : ControllerBase
{
     private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult GetDepartment()
        {
            var result = this.departmentService.GetAll();
            return Ok(new BaseResponseView<List<DepartmentResponseViewModel>>() { data = result });
        }

        [HttpGet("{id}")]
        public IActionResult GetDepartmentById(long id)
        {
            var result = this.departmentService.GetById(id);
            return Ok(new BaseResponseView<DepartmentResponseViewModel>() { data = result });
        }

         [HttpPost("")]
        public IActionResult CreateDepartment([FromBody] DepartmentRequestViewModel department)
        {
            var result = this.departmentService.Create(department);
            return Ok(new BaseResponseView<DepartmentResponseViewModel>() { data = result });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(long id, DepartmentRequestViewModel department)
        {
            var result = this.departmentService.Update(id, department);

            return Ok(new BaseResponseView<bool>() { data = result });
        }

         [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(long id)
        {
            var result = this.departmentService.Delete(id);

            return Ok(new BaseResponseView<bool>() { data = result });
        }


}
