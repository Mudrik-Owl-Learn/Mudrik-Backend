using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Commands.ToggleLessonStatus;
public class ToggleLessonStatusCommandValidator : AbstractValidator<ToggleLessonStatusCommand>
{
    public ToggleLessonStatusCommandValidator()
    {
        RuleFor(x => x.LessonId)
            .NotEmpty().WithMessage("معرف الدرس مطلوب.");
    }
}