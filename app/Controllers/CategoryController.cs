using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Services.Master;
using app.ViewModels.Master;
using dotnetapi.ViewModels.Base;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
  
    [Route("api/category")]
    [ApiController]
    [Produces("application/json")]
   public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        

        [HttpPost("")]
        public IActionResult Create(CategoryRequestViewModel category)
        {
            var result = this.categoryService.Create(category);
            return Ok(new BaseResponseView<CategoryResponseViewModel>() { data = result });
        }
    }
    
}