using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentQuizAnswer.Queries.GetQuestionAnswersByQuestionId
{
    internal class GetQuestionAnswersByQuestionIdQueryValidator : AbstractValidator<GetQuestionAnswersByQuestionIdQuery>
    {
        public GetQuestionAnswersByQuestionIdQueryValidator()
        {
            RuleFor(x => x.QuestionId)
         .NotEmpty().WithMessage("يجب ادخال رقم السؤال صحيح");
        }
    }
}
