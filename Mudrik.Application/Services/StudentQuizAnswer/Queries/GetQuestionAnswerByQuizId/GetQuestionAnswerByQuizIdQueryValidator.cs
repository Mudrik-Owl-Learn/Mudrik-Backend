using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentQuizAnswer.Queries.GetQuestionAnswerByQuizId
{
    public class GetQuestionAnswerByQuizIdQueryValidator : AbstractValidator<GetQuestionAnswerByQuizIdQuery>
    {
        public GetQuestionAnswerByQuizIdQueryValidator()
        {
            RuleFor(x => x.QuizId)
          .NotEmpty().WithMessage("يجب ادخال رقم الامتحان صحيح");
        }
    }
}
