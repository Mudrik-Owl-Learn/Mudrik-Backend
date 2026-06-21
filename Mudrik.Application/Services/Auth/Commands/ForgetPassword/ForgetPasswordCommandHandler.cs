using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Auth.Commands.ForgetPassword
{
    public class ForgetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
        : IRequestHandler<ForgetPasswordCommand, string>
    {
        public async Task<string> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new ValidationException("لا يوجد حساب مرتبط بهذا البريد الإلكتروني");

            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);

            return resetToken;
        }
    }
}
