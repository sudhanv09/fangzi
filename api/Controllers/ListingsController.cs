using api.Models;
using api.Models.DTO;
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
        int pageSize = 10)
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
        return listingById is null ? Results.NotFound() : Results.Ok(listingById);
    }
    
    [HttpPost("new")]
    [ValidateAntiForgeryToken]
    public async Task<IResult> NewListings([FromForm]ListingDto listing)
    {
        if (!ModelState.IsValid) return Results.BadRequest();
        
        await _listing.CreateNewListing(listing);
        return Results.Created();
    }
    
    
    [HttpPatch("update")]
    public async Task<IResult> UpdateListings([FromForm]ListingDto listing)
    {
        if (!ModelState.IsValid) return Results.BadRequest();
        return Results.Ok();
    }
    
    [HttpPost("remove")]
    public async Task<IResult> RemoveListings(string id)
    {
        return Results.Ok();
    }
}