using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class AuthController : Controller
{
    [Route("/logout")]
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        return Ok();
    }
}