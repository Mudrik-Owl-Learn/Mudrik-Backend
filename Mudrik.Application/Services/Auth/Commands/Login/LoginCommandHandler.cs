using MediatR;
using Microsoft.AspNetCore.Identity;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Auth.DTOs;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mudrik.Application.Services.Auth.Commands.Login
{
    public class LoginCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtService jwtService)
        : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(
        LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new ValidationException("البريد الإلكتروني أو كلمة المرور غير صحيحة");

            var signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);

            if (!signInResult.Succeeded)
            {
                var reason = signInResult.IsLockedOut ? "تم قفل الحساب مؤقتًا. حاول مرة أخرى لاحقًا." : "البريد الإلكتروني أو كلمة المرور غير صحيحة.";

                throw new ValidationException(reason);
            }

            var roles = await userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";

            var token = jwtService.GenerateToken(user, role);
            return new AuthResponseDto(token, role);
        }
    }
}
