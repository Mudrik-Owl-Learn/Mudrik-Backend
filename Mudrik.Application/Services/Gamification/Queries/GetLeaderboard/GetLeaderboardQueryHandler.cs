using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Gamification.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Gamification.Queries.GetLeaderboard
{
    public class GetLeaderboardQueryHandler(
        IGamificationStreakRepository streakRepository,
        IStudentDirectoryLookup studentDirectory)
        : IRequestHandler<GetLeaderboardQuery, LeaderboardDto>
    {
        public async Task<LeaderboardDto> Handle(GetLeaderboardQuery request, CancellationToken cancellationToken)
        {
            var gradeFilter = request.Scope == LeaderboardScope.ByGradeLevel ? request.GradeLevel : null;

            var topStreaks = await streakRepository.GetTopByPointsAsync(request.Top, gradeFilter, cancellationToken);

            var studentIds = topStreaks.Select(s => s.StudentProfileId).ToList();
            var basicInfoMap = await studentDirectory.GetBasicInfoAsync(studentIds, cancellationToken);

            var entries = new List<LeaderboardEntryDto>();
            var rank = 1;

            foreach (var streak in topStreaks)
            {
                var info = basicInfoMap.GetValueOrDefault(streak.StudentProfileId);

                entries.Add(new LeaderboardEntryDto(
                    Rank: rank,
                    StudentProfileId: streak.StudentProfileId,
                    DisplayName: info?.FirstName ?? "Unknown",
                    AvatarId: info?.AvatarId ?? "default",
                    TotalPoints: streak.TotalPoints,
                    CurrentLevel: streak.CurrentLevel
                ));

                rank++;
            }

            return new LeaderboardDto(entries);
        }
    }
}
