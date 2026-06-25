using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.Commands.CompleteQuiz
{
    public class CompleteQuizCommandValidator : AbstractValidator<CompleteQuizCommand>
    {
        public CompleteQuizCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("معرّف الاختبار مطلوب وغير صالح.");
        }
    }
}
