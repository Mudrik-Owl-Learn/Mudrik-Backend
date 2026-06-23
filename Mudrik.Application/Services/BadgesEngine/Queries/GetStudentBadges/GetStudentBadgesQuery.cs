using MediatR;
using Mudrik.Application.Services.BadgesEngine.DTOs;
using System;

namespace Mudrik.Application.Services.Badges.Queries.GetStudentBadges
{
    public record GetStudentBadgesQuery(Guid StudentProfileId) : IRequest<StudentBadgesDto>;
}
