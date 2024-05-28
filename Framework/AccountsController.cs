using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using octo.Application.Interface;
using octo.Infrastructure.Services;
using octo.Presentation.Dtos;

namespace octo.Framework
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(LoginServices loginService) : ControllerBase
    {
        private readonly LoginServices _loginService = loginService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                await _loginService.RegisterUserAsync(registerDto);
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
