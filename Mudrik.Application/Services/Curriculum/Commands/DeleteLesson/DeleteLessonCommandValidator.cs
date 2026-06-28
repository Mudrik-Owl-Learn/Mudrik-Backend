using FluentValidation;
using Mudrik.Application.Services.Curriculum.Commands.CreateLesson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Commands.DeleteLesson;
public class DeleteLessonCommandValidator : AbstractValidator<DeleteLessonCommand>
{
    public DeleteLessonCommandValidator()
    {
        RuleFor(x => x.LessonId)
            .NotEmpty().WithMessage("معرف الدرس مطلوب.");
    }
}