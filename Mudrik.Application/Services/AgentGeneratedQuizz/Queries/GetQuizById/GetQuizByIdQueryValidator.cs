using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.Queries.GetQuizById
{
    public class GetQuizByIdQueryValidator : AbstractValidator<GetQuizByIdQuery>
    {
        public GetQuizByIdQueryValidator()
        {
            RuleFor(x => x.id)
               .NotEqual(Guid.Empty)
               .WithMessage("معرّف الاختبار مطلوب وغير صالح.");
        }
    }
}
