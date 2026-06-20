using MediatR;
using Mudrik.Application.Services.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Auth.Commands.Register
{
    public record RegisterCommand(string FullName, string Email, string PhoneNumber, string Password) : IRequest<AuthResponseDto>;
}
