using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Auth.DTOs;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Auth.Commands.GoogleLogin
{
    public class GoogleCommandHandler(
    UserManager<ApplicationUser> userManager,
    IGoogleAuthService googleAuthService,
    IJwtService jwtService) : IRequestHandler<GoogleCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(
            GoogleCommand request, CancellationToken cancellationToken)
        {
            var googleUser = await googleAuthService.ValidateGoogleTokenAsync(request.IdToken);
            if (googleUser is null) throw new ValidationException("Invalid Google token.");

            var user = await userManager.FindByEmailAsync(googleUser.Email);

            if (user is null)
            {
                user = new ApplicationUser
                {
                    FullName = googleUser.FullName,
                    Email = googleUser.Email,
                    UserName = googleUser.Email,
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    var errors = string.Join(" ", createResult.Errors.Select(e => e.Description));
                    throw new ValidationException(errors);
                }

                await userManager.AddToRoleAsync(user, "User");
            }

            var roles = await userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";

            var token = jwtService.GenerateToken(user, role);
            return new AuthResponseDto(token, role);
        }
    }

}
