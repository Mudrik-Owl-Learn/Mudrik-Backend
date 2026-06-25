using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.LearnerProfile.Commands.CreateLearnerAIProfile;
using Mudrik.Application.Services.LearnerProfile.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.LearnerProfile.Queries.GetLearnerAIProfile
{
    public class GetLearnerAIProfileQueryHandler(ILearnerAIProfileRepository profileRepository)
        : IRequestHandler<GetLearnerAIProfileQuery, LearnerAIProfileDto?>
    {
        public async Task<LearnerAIProfileDto?> Handle(
            GetLearnerAIProfileQuery request, CancellationToken cancellationToken)
        {
            var profile = await profileRepository.GetByStudentProfileIdAsync(
                request.StudentProfileId, cancellationToken);

            return profile is null ? null : CreateLearnerAIProfileCommandHandler.MapToDto(profile);
        }
    }
}
