using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Queries.GetLessons;
public class GetLessonsQueryValidator : AbstractValidator<GetLessonsQuery>
{
    public GetLessonsQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);

        When(x => x.GradeLevel.HasValue, () =>
            RuleFor(x => x.GradeLevel!.Value).InclusiveBetween(1, 4));

        When(x => x.SearchTerm != null, () =>
            RuleFor(x => x.SearchTerm!).MaximumLength(200));
    }
}