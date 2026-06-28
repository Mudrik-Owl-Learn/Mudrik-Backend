using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Commands.ImportLessons;
public class ImportLessonsCommandValidator : AbstractValidator<ImportLessonsCommand>
{
    public ImportLessonsCommandValidator()
    {
        RuleFor(x => x.Lessons)
            .NotEmpty().WithMessage("قائمة الدروس فارغة.")
            .Must(l => l.Count <= 500).WithMessage("لا يمكن استيراد أكثر من 500 درس في المرة الواحدة.");

        RuleForEach(x => x.Lessons).ChildRules(lesson =>
        {
            lesson.RuleFor(l => l.SubjectId).NotEmpty().WithMessage("معرف المادة الدراسية مطلوب.");
            lesson.RuleFor(l => l.GradeLevel).InclusiveBetween(1, 4);
            lesson.RuleFor(l => l.ChapterNumber).GreaterThan(0);
            lesson.RuleFor(l => l.LessonOrder).GreaterThan(0);
            lesson.RuleFor(l => l.Title).NotEmpty().MaximumLength(500);
            lesson.RuleFor(l => l.RawContentText).NotEmpty();
        });
    }
}