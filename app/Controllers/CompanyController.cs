
using dotnetapi.Services.User;
using dotnetapi.ViewModels.Base;
using dotnetapi.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapi.Controllers;

[Route("api/company")]
[ApiController]
[Produces("application/json")]
public class CompanyController : ControllerBase
{
     private readonly ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpGet]
        public IActionResult GetCompany()
        {
            var result = this.companyService.GetAll();
            return Ok(new BaseResponseView<List<CompanyResponseViewModel>>() { data = result });
        }

        [HttpGet("{id}")]
        public IActionResult GetCompanyById(long id)
        {
            var result = this.companyService.GetById(id);
            return Ok(new BaseResponseView<CompanyResponseViewModel>() { data = result });
        }

         [HttpPost("")]
        public IActionResult CreateCompany([FromBody] CompanyRequestViewModel company)
        {
            var result = this.companyService.Create(company);
            return Ok(new BaseResponseView<CompanyResponseViewModel>() { data = result });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCompany(long id, CompanyRequestViewModel company)
        {
            var result = this.companyService.Update(id, company);

            return Ok(new BaseResponseView<bool>() { data = result });
        }

         [HttpDelete("{id}")]
        public IActionResult DeleteCompany(long id)
        {
            var result = this.companyService.Delete(id);

            return Ok(new BaseResponseView<bool>() { data = result });
        }


}
