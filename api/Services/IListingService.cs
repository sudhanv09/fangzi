using api.Models;
using api.Models.DTO;

namespace api.Services;

public interface IListingService
{
    Task<PagedList<ListingResponse>> GetAllListings(
        string? search,
        string? sortOrder,
        string? sortColumn,
        int page,
        int pageSize);
    Task<Listing> GetListingById(Guid id);

    Task<bool> CreateNewListing(ListingDto listingDto);
    Task BuildNerf();
    Task<Listing> UpdateListing(ListingDto listingDto);
    Task<bool> RemoveListing(string id);
}