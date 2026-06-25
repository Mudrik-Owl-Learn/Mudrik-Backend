using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.LearnerProfile.Commands.CreateLearnerAIProfile;
using Mudrik.Application.Services.LearnerProfile.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.LearnerProfile.Commands.UpdateLearnerAIProfile
{
    public class UpdateLearnerAIProfileCommandHandler(ILearnerAIProfileRepository profileRepository)
        : IRequestHandler<UpdateLearnerAIProfileCommand, LearnerAIProfileDto>
    {
        public async Task<LearnerAIProfileDto> Handle(
            UpdateLearnerAIProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await profileRepository.GetByStudentProfileIdAsync(
                request.StudentProfileId, cancellationToken)
                ?? throw new ValidationException(
                    "LearnerAIProfile not found for this student. Create it first via CreateLearnerAIProfileCommand.");

            // Domain method increments ProfileVersion and sets LastUpdatedAt automatically.
            profile.Update(
                request.DyslexiaSeverity,
                request.ADHDSeverity,
                request.ReadingScore,
                request.WritingScore,
                request.ComprehensionScore,
                request.AttentionSpanScore,
                request.AttentionSpanMinutes,
                request.PreferredFormat,
                request.ChunkSizePref,
                request.ConfidenceBias,
                request.AudioSupportRequired,
                request.NumeracyLevel,
                request.ReadingLevel,
                request.DiagnosticResultJson);

            await profileRepository.UpdateAsync(profile, cancellationToken);

            return CreateLearnerAIProfileCommandHandler.MapToDto(profile);
        }
    }
}
