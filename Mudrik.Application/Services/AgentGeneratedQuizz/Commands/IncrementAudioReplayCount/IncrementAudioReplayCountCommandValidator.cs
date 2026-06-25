using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.Commands.IncrementAudioReplayCount
{
    public class IncrementAudioReplayCountCommandValidator : AbstractValidator<IncrementAudioReplayCountCommand>
    {
        public IncrementAudioReplayCountCommandValidator()
        {
            RuleFor(x => x.id)
               .NotEqual(Guid.Empty)
               .WithMessage("معرّف الاختبار مطلوب وغير صالح.");
        }
    }
}
