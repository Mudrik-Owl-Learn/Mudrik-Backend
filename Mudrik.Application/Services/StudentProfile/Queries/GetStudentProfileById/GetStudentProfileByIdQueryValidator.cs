using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mudrik.Application.Services.StudentProfile.Queries.GetStudentProfileById
{
    public class GetStudentProfileByIdQueryValidator : AbstractValidator<GetStudentProfileByIdQuery>
    {
        public GetStudentProfileByIdQueryValidator()
        {
            RuleFor(x => x.id)
           .GreaterThan(0).WithMessage("Student profile ID must be a positive integer.");
        }
    }
}
