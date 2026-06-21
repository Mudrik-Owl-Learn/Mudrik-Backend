using MediatR;
using Mudrik.Application.Services.Gamification.DTOs;

namespace Mudrik.Application.Services.Gamification.Queries.GetXpTransactionById
{
    public record GetXpTransactionByIdQuery(Guid Id) : IRequest<XpTransactionDto?>;
}
