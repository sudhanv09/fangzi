using api.Models;

namespace api.Services;

public interface IListingService
{
    Task<List<Listing>> GetAllListings();
    Task<Listing> GetListingById(string id);

    Task<Listing> CreateNewListing();
    Task CreateNerf();
    Task<Listing> UpdateListing(Listing listing);
    Task RemoveListing();
}