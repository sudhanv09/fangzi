namespace api.Models;

public class UserResponse
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<Listing> Listings { get; set; }
}