using FluentValidation;
using System;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.Commands.Generatequiz
{
    public class GenerateQuizCommandValidator : AbstractValidator<GenerateQuizCommand>
    {
        public GenerateQuizCommandValidator()
        {
            RuleFor(x => x.StudentProfileId)
                .NotEqual(Guid.Empty)
                .WithMessage("معرّف ملف الطالب مطلوب وغير صالح.");

            RuleFor(x => x.SubjectId)
                .NotEqual(Guid.Empty)
                .WithMessage("معرّف المادة الدراسية مطلوب وغير صالح.");

            // At least one lesson reference must be provided
            RuleFor(x => x)
                .Must(x => x.LessonMicroChunkId != Guid.Empty || x.StandardLessonId != Guid.Empty)
                .WithMessage("يجب تحديد جزء الدرس (LessonMicroChunkId) أو الدرس الأساسي (StandardLessonId).");

            RuleFor(x => x.ConceptTag)
                .MaximumLength(100)
                .WithMessage("وسم المفهوم يجب ألا يتجاوز 100 حرف.")
                .When(x => !string.IsNullOrWhiteSpace(x.ConceptTag));

            RuleFor(x => x.GradeLevel)
                .InclusiveBetween(1, 4)
                .WithMessage("المستوى الدراسي يجب أن يكون بين 1 و 4.");

            RuleFor(x => x.TotalTimeSeconds)
                .GreaterThanOrEqualTo(0)
                .WithMessage("الزمن الإجمالي لا يمكن أن يكون سالباً.");
        }
    }
}