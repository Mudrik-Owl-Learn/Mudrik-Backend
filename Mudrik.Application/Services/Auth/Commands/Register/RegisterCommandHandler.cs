using MediatR;
using Microsoft.AspNetCore.Identity;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Auth.DTOs;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mudrik.Application.Services.Auth.Commands.Register
{
    public class RegisterCommandHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        : IRequestHandler<RegisterCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(
        RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser is not null) throw new ValidationException("البريد الإلكتروني مسجل بالفعل");

            var user = new ApplicationUser
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            var createResult = await userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(" ", createResult.Errors.Select(e => e.Description));
                throw new ValidationException(errors);
            }

            await userManager.AddToRoleAsync(user, "User");

            var token = jwtService.GenerateToken(user, "User");
            return new AuthResponseDto(token, "User");
        }
    }
}
