using FluentValidation;
using System;

namespace Mudrik.Application.Services.QuizQuestion.Commands.DeleteQuizQuestion
{
    public class DeleteQuizQuestionCommandValidator : AbstractValidator<DeleteQuizQuestionCommand>
    {
        public DeleteQuizQuestionCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEqual(Guid.Empty)
                .WithMessage("معرّف السؤال مطلوب وغير صالح.");
        }
    }
}