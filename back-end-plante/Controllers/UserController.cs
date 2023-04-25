using Microsoft.AspNetCore.Mvc;

namespace back_end_plante.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{


    [HttpGet("login")]
    public IActionResult Login([FromQuery] string email, [FromQuery] string password)
    {
        return Challenge();
    }
}