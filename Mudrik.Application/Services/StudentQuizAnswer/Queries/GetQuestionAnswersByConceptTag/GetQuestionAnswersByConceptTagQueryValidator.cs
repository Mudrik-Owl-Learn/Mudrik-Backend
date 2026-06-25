using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentQuizAnswer.Queries.GetQuestionAnswersByConceptTag
{
    internal class GetQuestionAnswersByConceptTagQueryValidator : AbstractValidator<GetQuestionAnswersByConceptTagQuery>
    {
        public GetQuestionAnswersByConceptTagQueryValidator()
        {
            RuleFor(x => x.ConceptTag)  
         .NotEmpty().WithMessage("يجب ادخال موضوع درس صحيح");

        }
    }
}
