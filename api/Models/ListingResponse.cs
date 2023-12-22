namespace api.Models;

public class ListingResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public int PostalCode { get; set; }
    public string Cost { get; set; }
    public string Images { get; set; }
    public List<string>? Utilities { get; set; }
    public List<string>? Meta { get; set; }
    public bool Status { get; set; }
    public DateTime PostDate { get; set; } = DateTime.UtcNow;
}