using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Auth.Commands.GoogleLogin
{
    public class GoogleCommandValidator : AbstractValidator<GoogleCommand>
    {
        public GoogleCommandValidator()
        {
            RuleFor(x => x.IdToken)
                .NotEmpty().WithMessage("Google ID token is required.");
        }
    }
}
