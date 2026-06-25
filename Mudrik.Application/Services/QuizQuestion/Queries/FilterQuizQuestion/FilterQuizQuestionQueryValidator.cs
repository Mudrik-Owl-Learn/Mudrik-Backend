using FluentValidation;
using System;

namespace Mudrik.Application.Services.QuizQuestion.Queries.FilterQuizQuestion
{
    public class FilterQuizQuestionQueryValidator : AbstractValidator<FilterQuizQuestionQuery>
    {
        public FilterQuizQuestionQueryValidator()
        {
            // At least one filter criterion must be provided — otherwise this
            // becomes "return everything", which should go through a dedicated
            // GetAll/paged endpoint instead, not this filter query.
            RuleFor(x => x)
                .Must(HasAtLeastOneFilter)
                .WithMessage("يجب تحديد معيار تصفية واحد على الأقل (الدرس، أو وسم المفهوم، أو المستوى الدراسي).");

            RuleFor(x => x.StandardLessonId)
                .Must(id => id == Guid.Empty || id != Guid.Empty) // placeholder to anchor rule chain
                .DependentRules(() =>
                {
                    RuleFor(x => x.StandardLessonId)
                        .NotEqual(Guid.Empty)
                        .WithMessage("معرّف الدرس الأساسي غير صالح.")
                        .When(x => x.StandardLessonId != Guid.Empty);
                });

            RuleFor(x => x.ConceptTag)
                .MaximumLength(100)
                .WithMessage("وسم المفهوم يجب ألا يتجاوز 100 حرف.")
                .When(x => !string.IsNullOrWhiteSpace(x.ConceptTag));

            RuleFor(x => x.GradeLevel)
                .InclusiveBetween(1, 4)
                .WithMessage("المستوى الدراسي يجب أن يكون بين 1 و 4.")
                .When(x => x.GradeLevel != 0);
        }

        private bool HasAtLeastOneFilter(FilterQuizQuestionQuery query)
        {
            return query.StandardLessonId != Guid.Empty
                || !string.IsNullOrWhiteSpace(query.ConceptTag)
                || query.GradeLevel != 0;
        }
    }
}