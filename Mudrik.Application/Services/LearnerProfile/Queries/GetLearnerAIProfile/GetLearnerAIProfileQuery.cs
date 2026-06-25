using MediatR;
using Mudrik.Application.Services.LearnerProfile.DTOs;
using System;

namespace Mudrik.Application.Services.LearnerProfile.Queries.GetLearnerAIProfile
{
    public record GetLearnerAIProfileQuery(Guid StudentProfileId) : IRequest<LearnerAIProfileDto?>;
}
