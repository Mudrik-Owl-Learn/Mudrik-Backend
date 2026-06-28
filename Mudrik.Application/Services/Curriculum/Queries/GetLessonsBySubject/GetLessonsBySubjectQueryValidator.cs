using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Queries.GetLessonsBySubject;
public class GetLessonsBySubjectQueryValidator : AbstractValidator<GetLessonsBySubjectQuery>
{
    public GetLessonsBySubjectQueryValidator()
    {
        RuleFor(x => x.SubjectId)
            .NotEmpty().WithMessage("معرف المادة الدراسية مطلوب.");
    }
}