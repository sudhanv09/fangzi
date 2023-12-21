using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("listing")]
public class ListingsController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAllListings()
    {
        return Ok();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetListings(string id)
    {
        return Ok();
    }
    
    [HttpPost("new")]
    public async Task<IActionResult> NewListings([FromForm]Listing listing)
    {
        return Ok();
    }
    
    
    [HttpPatch("update")]
    public async Task<IActionResult> UpdateListings()
    {
        return Ok();
    }
    
    [HttpPost("remove")]
    public async Task<IActionResult> RemoveListings()
    {
        return Ok();
    }
}