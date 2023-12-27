using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.API.Auth
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Login (AuthRequestDto requestDto)
        {
            var response = _authService.CreateToken(requestDto);
            return Ok(response);
        }
        
    
    }
}
