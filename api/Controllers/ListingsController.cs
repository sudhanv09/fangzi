using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class ListingsController : Controller
{
    [Route("/")]
    [HttpGet]
    public async Task<IActionResult> AllListings()
    {
        return Ok();
    }
}