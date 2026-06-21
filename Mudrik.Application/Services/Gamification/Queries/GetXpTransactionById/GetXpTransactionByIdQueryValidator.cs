using FluentValidation;

namespace Mudrik.Application.Services.Gamification.Queries.GetXpTransactionById
{
    public class GetXpTransactionByIdQueryValidator : AbstractValidator<GetXpTransactionByIdQuery>
    {
        public GetXpTransactionByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}
