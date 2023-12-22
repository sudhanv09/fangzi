using System.Security.Claims;
using api.Models;
using api.Models.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("auth")]
[ApiController]
public class AuthController(SignInManager<User> signIn, UserManager<User> userManager) : Controller
{
    private SignInManager<User> _SignIn { get; set; } = signIn;
    public UserManager<User> _UserManager { get; set; } = userManager;

    [HttpPost("login")]
    public async Task<IResult> Login([FromBody]LoginDto login)
    {
        if (!ModelState.IsValid) return Results.BadRequest("Input Invalid");

        User userExists = await _UserManager.FindByEmailAsync(login.Email);
        if (userExists is null) return Results.NotFound();

        var userLogin = await _SignIn.PasswordSignInAsync(userExists, login.Password, true, false);
        if (!userLogin.Succeeded) return Results.Unauthorized();
        
        return Results.Ok();
    }
    
    [HttpPost("register")]
    public async Task<IResult> Register([FromBody]RegisterDto register)
    {
        if (!ModelState.IsValid) return Results.BadRequest("Input Invalid");

        var user = new User()
        {
            Email = register.Email,
            NormalizedEmail = register.Email.ToUpper(),
            UserName = register.Name,
            NormalizedUserName = register.Name.ToUpper(),
        };

        var result = await _UserManager.CreateAsync(user, register.Password);
        if (!result.Succeeded)
        {
            foreach (var resultError in result.Errors)
            {
                return Results.BadRequest(resultError.Description);
            }
        }
        return Results.Created();
    }
    
    [HttpPost("logout")]
    public async Task<IResult> Logout()
    {
        await _SignIn.SignOutAsync();
        return Results.SignOut();
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<IResult> GetUserInfo()
    {
        var userName = User.Identity.Name;
        var userExists = await _UserManager.FindByNameAsync(userName);
        if (userExists is null) return Results.NotFound();

        var userResponse = new UserResponse()
        {
            Id = userExists.Id,
            UserName = userExists.UserName,
            Email = userExists.Email,
            Listings = userExists.UserListings
        };

        return Results.Ok(userResponse);
    }
}