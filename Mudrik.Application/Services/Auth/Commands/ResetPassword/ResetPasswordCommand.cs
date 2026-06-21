using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Auth.Commands.ResetPassword
{
    public record ResetPasswordCommand(string Email, string Token, string NewPassword) : IRequest;
}
