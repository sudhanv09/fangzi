using System.Linq.Expressions;
using api.DbContext;
using api.Models;
using api.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class ListingService(AppDbContext dbContext, ILogger<ListingService> logger) : IListingService
{
    
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
        IQueryable<Listing> listingQuery = dbContext.Listings;
        
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
        // TODO load images if exists from localstorage
        var listingsResponseQuery = listingQuery.Select(r => new ListingResponse()
        {
            Id = r.Id,
            Title = r.Title,
            Address = r.Address,
            City = r.City,
            PostalCode = r.PostalCode,
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
        var listingId = await dbContext.Listings.FirstOrDefaultAsync(i => i.Id == id);
        return listingId;
    }

    public async Task<bool> CreateNewListing(ListingDto listingDto)
    {
        try
        {
            // TODO Save images to local disk
            var newListing = new Listing()
            {
                Title = listingDto.Title,
                Address = listingDto.Address,
                City = listingDto.City,
                PostalCode = listingDto.PostalCode,
                Cost = listingDto.Cost,
                Description = listingDto.Description,
                Meta = listingDto.Meta,
                Status = false,
                Utilities = listingDto.Utilities
            };

            await dbContext.Listings.AddAsync(newListing);
            await dbContext.SaveChangesAsync();

            await BuildNerf();
            return true;
        }

        catch (DbUpdateException dbExcept)
        {
            logger.LogError(dbExcept, "There was an exception when saving listings");
            return false;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while creating a new listing");
            return false;
        }
    }

    public async Task BuildNerf()
    {
        throw new NotImplementedException();
    }

    public async Task<Listing> UpdateListing(ListingDto listingDto)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveListing(string id)
    {
        try
        {
            var getListing = await dbContext.Listings.FindAsync(id);
            if (getListing is null) return false;
            
            dbContext.Listings.Remove(getListing);
            await dbContext.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException dbExcept)
        {
            logger.LogError(dbExcept, "There was an exception when removing listings");
            return false;
        }
    }
}