namespace api.Models.DTO;

public class ListingDto
{
    public string UserId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public int PostalCode { get; set; }
    public string Cost { get; set; }
    public List<IFormFile>? Images { get; set; }
    public List<string>? Utilities { get; set; }
    public List<string>? Meta { get; set; }
}