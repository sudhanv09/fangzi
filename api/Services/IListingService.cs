using api.Models;

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

    Task<Listing> CreateNewListing();
    Task CreateNerf();
    Task<Listing> UpdateListing(Listing listing);
    Task RemoveListing();
}