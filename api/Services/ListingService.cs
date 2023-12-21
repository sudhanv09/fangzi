using api.Models;

namespace api.Services;

public class ListingService : IListingService
{
    public Task<List<Listing>> GetAllListings()
    {
        throw new NotImplementedException();
    }

    public Task<Listing> GetListingById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Listing> CreateNewListing()
    {
        throw new NotImplementedException();
    }

    public Task CreateNerf()
    {
        throw new NotImplementedException();
    }

    public Task<Listing> UpdateListing(Listing listing)
    {
        throw new NotImplementedException();
    }

    public Task RemoveListing()
    {
        throw new NotImplementedException();
    }
}