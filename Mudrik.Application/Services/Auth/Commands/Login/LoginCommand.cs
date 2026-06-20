using MediatR;
using Mudrik.Application.Services.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Auth.Commands.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<AuthResponseDto>;
}
