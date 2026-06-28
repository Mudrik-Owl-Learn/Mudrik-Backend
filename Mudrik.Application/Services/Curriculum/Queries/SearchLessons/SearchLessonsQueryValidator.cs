using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Queries.SearchLessons;
public class SearchLessonsQueryValidator : AbstractValidator<SearchLessonsQuery>
{
    public SearchLessonsQueryValidator()
    {
        RuleFor(x => x.Term)
            .NotEmpty().WithMessage("نص البحث مطلوب.")
            .MinimumLength(2).WithMessage("نص البحث يجب أن يكون حرفين على الأقل.")
            .MaximumLength(200);
    }
}