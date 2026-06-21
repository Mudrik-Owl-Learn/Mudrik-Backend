using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mudrik.Application.Services.Auth.Commands.ForgetPassword;
using Mudrik.Application.Services.Auth.Commands.GoogleLogin;
using Mudrik.Application.Services.Auth.Commands.Login;
using Mudrik.Application.Services.Auth.Commands.Register;
using Mudrik.Application.Services.Auth.Commands.ResetPassword;

namespace Mudrik.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordCommand command)
        {
            var token = await mediator.Send(command);
            return Ok(new { resetToken = token });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}
