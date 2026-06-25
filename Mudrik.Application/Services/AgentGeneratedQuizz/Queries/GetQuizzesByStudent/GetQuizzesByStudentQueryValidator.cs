using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.Queries.GetQuizzesByStudent
{
    public class GetQuizzesByStudentQueryValidator : AbstractValidator<GetQuizzesByStudentQuery>
    {

        public GetQuizzesByStudentQueryValidator()
        {
            RuleFor(x => x.studentId)
                .NotEqual(Guid.Empty)
                .WithMessage("معرّف الطالب مطلوب وغير صالح.");

        }


    }
}
