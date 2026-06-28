using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Queries.GetLessonById;
public class GetLessonByIdQueryValidator : AbstractValidator<GetLessonByIdQuery>
{
    public GetLessonByIdQueryValidator()
    {
        RuleFor(x => x.LessonId)
            .NotEmpty().WithMessage("معرف الدرس مطلوب.");
    }
}