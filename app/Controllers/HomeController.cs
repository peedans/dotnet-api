using Microsoft.AspNetCore.Mvc;
using dotnetapi.Models.Master;

namespace dotnetapi.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<HomeController> logger;

    public DBMasterContext dbContext { get; }

    public HomeController(ILogger<HomeController> logger, DBMasterContext dBContext)
    {
        this.logger = logger;
        dbContext = dBContext;
    }

    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        var result = dbContext.BlogPost;
        return Ok();
    }
}
