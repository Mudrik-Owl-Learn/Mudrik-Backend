using MediatR;
using Mudrik.Application.Interfaces.CurriculumService;
using Mudrik.Application.Services.Curriculum.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Queries.GetCurriculumStats;
public class GetCurriculumStatsQueryHandler(ICurriculumRepository repository)
        : IRequestHandler<GetCurriculumStatsQuery, CurriculumStatsDto>
{
    public async Task<CurriculumStatsDto> Handle(
        GetCurriculumStatsQuery request, CancellationToken cancellationToken)
    {
        var stats = await repository.GetStatsAsync(cancellationToken);

        var subjectSummaries = stats.BySubject
            .Select(s => new SubjectSummaryDto(
                SubjectId: s.SubjectId,
                Title: s.Title,
                IconUrl: s.IconUrl,
                DisplayOrder: s.DisplayOrder,
                TotalLessons: s.TotalLessons,
                ActiveLessons: s.ActiveLessons,
                StatusSummary: BuildStatusSummary(s.TotalLessons, s.ActiveLessons)))
            .OrderBy(s => s.DisplayOrder)
            .ToList()
            .AsReadOnly();

        return new CurriculumStatsDto(
            TotalSubjects: stats.TotalSubjects,
            TotalLessons: stats.TotalLessons,
            ActiveLessons: stats.ActiveLessons,
            InactiveLessons: stats.InactiveLessons,
            TotalGrades: stats.TotalGrades,
            SubjectSummaries: subjectSummaries);
    }

    private static string BuildStatusSummary(int total, int active)
    {
        if (total == 0) return "لا يوجد دروس";
        if (active == total) return "نشط";
        if (active == 0) return "مسودة";
        return "مسودة جزئياً";
    }
}