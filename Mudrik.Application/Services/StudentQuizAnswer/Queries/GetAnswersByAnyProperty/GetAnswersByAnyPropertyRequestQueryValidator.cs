using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentQuizAnswer.Queries.GetAnswersByAnyProperty
{
    public class GetAnswersByAnyPropertyRequestQueryValidator : AbstractValidator<GetAnswersByAnyPropertyRequestQuery>
    {
        public GetAnswersByAnyPropertyRequestQueryValidator()
        {
            RuleFor(x => x.StudentProfileId)
               .NotEqual(Guid.Empty)
               .WithMessage("معرّف الطالب مطلوب وغير صالح.");

            RuleFor(x => x)
                .Must(x => !x.From.HasValue || !x.To.HasValue || x.From <= x.To)
                .WithMessage("تاريخ البداية يجب أن يكون قبل تاريخ النهاية أو يساويه.");

            RuleFor(x => x.ConceptTag)
                .MaximumLength(100)
                .WithMessage("وسم المفهوم يجب ألا يتجاوز 100 حرف.")
                .When(x => !string.IsNullOrWhiteSpace(x.ConceptTag));

        }
    }
}
