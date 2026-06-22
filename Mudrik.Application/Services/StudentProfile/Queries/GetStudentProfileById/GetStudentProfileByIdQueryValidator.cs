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
           .NotEmpty().WithMessage("يجب ادخال رقم هوية او معرف صحيح");
        }
    }
}
