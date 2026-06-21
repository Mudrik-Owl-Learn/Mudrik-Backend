using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentProfile.Commands.DeleteStudentProfile
{
    public class DeleteStudentProfileCommandValidator : AbstractValidator<DeleteStudentProfileCommand>
    {
        public DeleteStudentProfileCommandValidator()
        {
            RuleFor(x => x.id)
           .GreaterThan(0).WithMessage("Student profile ID must be a positive integer.");
        }
    }
}
