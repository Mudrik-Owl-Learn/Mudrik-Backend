using FluentValidation;
using System;

namespace Mudrik.Application.Services.QuizQuestion.Queries.GetQuizQuestionById
{
    public class GetQuizQuestionByIdQueryValidator : AbstractValidator<GetQuizQuestionByIdQuery>
    {
        public GetQuizQuestionByIdQueryValidator()
        {
            RuleFor(x => x.id)
                .NotEqual(Guid.Empty)
                .WithMessage("معرّف السؤال مطلوب وغير صالح.");
        }
    }
}