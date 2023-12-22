using Microsoft.AspNetCore.Identity;

namespace api.Models;

public class User : IdentityUser
{
    public List<Listing>? UserListings { get; set; }
}