using dotnetapi.Services;
using dotnetapi.ViewModels.Base;
using Microsoft.AspNetCore.Mvc;
using dotnetapi.ViewModels.User;
using dotnetapi.Services.User;

namespace dotnetapi.Controllers
{

    [Route("api/salary")]
    [ApiController]
    [Produces("application/json")]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService salaryService;

        public SalaryController(ISalaryService salaryService)
        {
            this.salaryService = salaryService;
        }
        [HttpGet("")]
        public IActionResult GetSalarys()
        {
            var result = this.salaryService.GetAll();
            return Ok(new BaseResponseView<List<SalaryResponseViewModel>>() { data = result });
        }
        
        [HttpGet("{id}")]
        public IActionResult GetSalaryById(long id)
        {
            var result = this.salaryService.GetById(id);
            return Ok(new BaseResponseView<SalaryResponseViewModel>() { data = result });
        }

         [HttpPost("")]
        public IActionResult CreateSalary([FromBody] SalaryRequestViewModel salary)
        {
            var result = this.salaryService.Create(salary);
            return Ok(new BaseResponseView<SalaryResponseViewModel>() { data = result });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSalary(long id, SalaryRequestUpdateViewModel salary)
        {
            var result = this.salaryService.Update(id, salary);

            return Ok(new BaseResponseView<bool>() { data = result });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSalary(long id)
        {
            var result = this.salaryService.Delete(id);

            return Ok(new BaseResponseView<bool>() { data = result });
        }

        [HttpPost("salaryhitory")]
        public IActionResult CreateSalaryHistory([FromBody] SalaryHistoryRequestViewModel salaryHistory)
        {
            var result = this.salaryService.CreateSalaryHistory(salaryHistory);
            return Ok(new BaseResponseView<SalaryHistoryResponseViewModel>() { data = result });
        }
    }
}