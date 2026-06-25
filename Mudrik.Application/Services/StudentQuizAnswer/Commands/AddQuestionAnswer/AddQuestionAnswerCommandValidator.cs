using FluentValidation;
using System;

namespace Mudrik.Application.Services.StudentQuizAnswer.Commands.AddQuestionAnswer
{
    public class AddQuestionAnswerCommandValidator : AbstractValidator<AddQuestionAnswerCommand>
    {
        public AddQuestionAnswerCommandValidator()
        {
            RuleFor(x => x.AgentGeneratedQuizId)
                .NotEqual(Guid.Empty)
                .WithMessage("معرّف الاختبار مطلوب وغير صالح.");

            RuleFor(x => x.StudentProfileId)
                .NotEqual(Guid.Empty)
                .WithMessage("معرّف ملف الطالب مطلوب وغير صالح.");

            RuleFor(x => x.QuizQuestionId)
                .NotEqual(Guid.Empty)
                .WithMessage("معرّف السؤال مطلوب وغير صالح.");

            RuleFor(x => x.SelectedOptionId)
                .NotEmpty()
                .WithMessage("يجب اختيار إجابة.")
                .MaximumLength(50)
                .WithMessage("معرّف الخيار المحدد غير صالح.");

            RuleFor(x => x.TimeToAnswerMs)
                .GreaterThanOrEqualTo(0)
                .WithMessage("زمن الإجابة غير صالح.")
                .LessThanOrEqualTo(600000) // 1/2 hour ceiling — adjust if needed
                .WithMessage("زمن الإجابة يبدو غير منطقي (يتجاوز الحد المسموح).");

            RuleFor(x => x.AnswerChangeCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("عدد مرات تغيير الإجابة لا يمكن أن يكون سالباً.");
        }
    }
}