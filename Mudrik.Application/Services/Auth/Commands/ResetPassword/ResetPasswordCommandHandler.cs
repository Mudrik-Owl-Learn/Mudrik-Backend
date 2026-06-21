using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Identity;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<ResetPasswordCommand>
    {
        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new ValidationException("لا يوجد حساب مرتبط بهذا البريد الإلكتروني");

            var result = await userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(" ", result.Errors.Select(e => e.Description));
                throw new ValidationException(errors);
            }
        }
    }
}
