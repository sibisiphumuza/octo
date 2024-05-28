using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
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
                return CreatedAtAction(nameof(Register), new { username = registerDto.Username }, "User registered successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while registering the user.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(RegisterDto registerDto)
        {
            try
            {
                var user = await _loginService.LoginAsync(registerDto.Username, registerDto.Password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
