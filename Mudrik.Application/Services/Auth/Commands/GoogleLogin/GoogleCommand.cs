using MediatR;
using Mudrik.Application.Services.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Auth.Commands.GoogleLogin
{
    public record GoogleCommand(string IdToken) : IRequest<AuthResponseDto>;
}
