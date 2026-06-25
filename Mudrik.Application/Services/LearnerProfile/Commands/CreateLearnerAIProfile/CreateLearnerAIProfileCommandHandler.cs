using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.LearnerProfile.DTOs;
using Mudrik.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.LearnerProfile.Commands.CreateLearnerAIProfile
{
    public class CreateLearnerAIProfileCommandHandler(ILearnerAIProfileRepository profileRepository)
        : IRequestHandler<CreateLearnerAIProfileCommand, LearnerAIProfileDto>
    {
        public async Task<LearnerAIProfileDto> Handle(
            CreateLearnerAIProfileCommand request, CancellationToken cancellationToken)
        {
            var existing = await profileRepository.GetByStudentProfileIdAsync(
                request.StudentProfileId, cancellationToken);

            if (existing is not null)
                throw new ValidationException(
                    "A LearnerAIProfile already exists for this student. Use UpdateLearnerAIProfileCommand instead.");

            var profile = LearnerAIProfile.Create(
                request.StudentProfileId,
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

            await profileRepository.AddAsync(profile, cancellationToken);

            return MapToDto(profile);
        }

        internal static LearnerAIProfileDto MapToDto(LearnerAIProfile p) => new(
            p.Id, p.StudentProfileId,
            p.DyslexiaSeverity, p.ADHDSeverity,
            p.ReadingScore, p.WritingScore, p.ComprehensionScore,
            p.AttentionSpanScore, p.AttentionSpanMinutes,
            p.PreferredFormat, p.ChunkSizePref, p.ConfidenceBias,
            p.AudioSupportRequired, p.NumeracyLevel, p.ReadingLevel,
            p.DiagnosticResultJson, p.ProfileVersion,
            p.LastUpdatedAt, p.CreatedAt);
    }
}
