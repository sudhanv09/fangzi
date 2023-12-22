using System.Linq.Expressions;
using api.DbContext;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class ListingService(AppDbContext dbContext) : IListingService
{
    private AppDbContext DbContext { get; set; } = dbContext;
    private static Expression<Func<Listing, object>> GetSortProperty(string sortColumn)
    {
        Expression<Func<Listing, object>> keySelector = sortColumn.ToLower() switch
        {
            "date" => listings => listings.PostDate,
            "cost" => listings => listings.Cost,
            "city" => listings => listings.City,
            
            _ => listings => listings.Id
        };
        return keySelector;
    }

    public async Task<PagedList<ListingResponse>> GetAllListings(string? search, string? sortOrder, string? sortColumn, int page, int pageSize)
    {
        IQueryable<Listing> listingQuery = DbContext.Listings;
        
        // Search
        if (!string.IsNullOrWhiteSpace(search))
        {
            listingQuery = listingQuery.Where(q => 
                q.Title.Contains(search) ||
                q.Address.Contains(search) ||
                q.Description.Contains(search)
            );
        }
        
        // Sorting
        var keySelector = GetSortProperty(sortColumn);

        if (sortOrder.ToLower() == "desc")
        {
            listingQuery = listingQuery.OrderByDescending(keySelector);
        }
        else
        {
            listingQuery = listingQuery.OrderBy(keySelector);
        }
        
        // Pagination
        var listingsResponseQuery = listingQuery.Select(r => new ListingResponse()
        {
            Id = r.Id,
            Title = r.Title,
            Address = r.Address,
            Cost = r.Cost,
            Description = r.Description,
            Meta = r.Meta,
            PostDate = r.PostDate,
            Status = r.Status,
            Utilities = r.Utilities
        });

        var listings = await PagedList<ListingResponse>.CreateAsync(listingsResponseQuery, page, pageSize);
        return listings;
    }

    public async Task<Listing> GetListingById(Guid id)
    {
        var listingId = await DbContext.Listings.FirstOrDefaultAsync(i => i.Id == id);
        return listingId;
    }

    public async Task<Listing> CreateNewListing()
    {
        throw new NotImplementedException();
    }

    public async Task CreateNerf()
    {
        throw new NotImplementedException();
    }

    public async Task<Listing> UpdateListing(Listing listing)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveListing()
    {
        throw new NotImplementedException();
    }
}