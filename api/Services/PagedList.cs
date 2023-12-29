using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class PagedList<T>
{
    private PagedList(List<T> item, int page, int pageSize, int totalCount)
    {
        Items = item;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    private int Page { get; set; }
    private int PageSize { get; set; }
    public List<T> Items { get; set; }
    private int TotalCount { get; set; }

    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPrevPage => Page > 1;

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        var totalCount = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new(items, page, pageSize, totalCount);
    }
}