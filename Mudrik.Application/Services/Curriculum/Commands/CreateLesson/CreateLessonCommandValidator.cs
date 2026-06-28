using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Commands.CreateLesson;
public class CreateLessonCommandValidator : AbstractValidator<CreateLessonCommand>
{
    public CreateLessonCommandValidator()
    {
        RuleFor(x => x.SubjectId)
            .NotEmpty().WithMessage("معرف المادة الدراسية مطلوب.");

        RuleFor(x => x.GradeLevel)
            .InclusiveBetween(1, 4).WithMessage("الصف الدراسي يجب أن يكون بين 1 و 4.");

        RuleFor(x => x.ChapterNumber)
            .GreaterThan(0).WithMessage("رقم الفصل يجب أن يكون أكبر من صفر.");

        RuleFor(x => x.LessonOrder)
            .GreaterThan(0).WithMessage("ترتيب الدرس يجب أن يكون أكبر من صفر.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان الدرس مطلوب.")
            .MaximumLength(500).WithMessage("عنوان الدرس لا يتجاوز 500 حرف.");

        RuleFor(x => x.RawContentText)
            .NotEmpty().WithMessage("محتوى الدرس مطلوب.");
    }
}