using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentQuizAnswer.Queries.GetQuestionAnswerByStudentId
{
    public class GetQuestionAnswersByStudentIdQueryValidator : AbstractValidator<GetQuestionAnswersByStudentIdQuery>
    {
        public GetQuestionAnswersByStudentIdQueryValidator()
        {
            RuleFor(x => x.StudentId)
         .NotEmpty().WithMessage("يجب ادخال رقم هوية او معرف صحيح");
        }
    }
}
