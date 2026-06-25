using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentQuizAnswer.Queries.GetQuestionAnswerById
{
    public class GetQuestionAnswerByIdQueryValidator : AbstractValidator<GetQuestionAnswerByIdQuery>
    {
        public GetQuestionAnswerByIdQueryValidator()
        {
            RuleFor(x => x.id)
           .NotEmpty().WithMessage("يجب ادخال رقم هوية او معرف صحيح");
        }
    }
}
