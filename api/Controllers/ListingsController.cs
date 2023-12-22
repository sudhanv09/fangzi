using api.Models;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("listing")]
[Authorize]

public class ListingsController(IListingService listingService) : Controller
{
    private IListingService _listing { get; set; } = listingService;
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IResult> GetListings(
        string? search,
        string? sortOrder,
        string? sortColumn,
        int page,
        int pageSize)
    {
        var getListings = await _listing.GetAllListings(search, sortOrder, sortColumn, page, pageSize);
        return Results.Ok(getListings);
    }
    
    [HttpGet("{id}")]
    public async Task<IResult> GetListings(string id)
    {
        bool isValid = Guid.TryParse(id, out Guid modId);
        if (!isValid) return Results.BadRequest("Bad Id");
        
        var listingById = await _listing.GetListingById(modId);
        return Results.Ok(listingById);
    }
    
    [HttpPost("new")]
    [ValidateAntiForgeryToken]
    public async Task<IResult> NewListings([FromForm]Listing listing)
    {
        return Results.Ok();
    }
    
    
    [HttpPatch("update")]
    public async Task<IResult> UpdateListings()
    {
        return Results.Ok();
    }
    
    [HttpPost("remove")]
    public async Task<IResult> RemoveListings()
    {
        return Results.Ok();
    }
}