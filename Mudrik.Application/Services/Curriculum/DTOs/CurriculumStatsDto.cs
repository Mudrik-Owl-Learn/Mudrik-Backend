using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.DTOs;
/// <summary>
/// KPI stats shown in the four stat cards at the top of the curriculum management page.
/// Includes per-subject breakdown for the subject tab strip.
/// </summary>
public record CurriculumStatsDto(
    int TotalSubjects,
    int TotalLessons,
    int ActiveLessons,
    int InactiveLessons,
    int TotalGrades,
    IReadOnlyList<SubjectSummaryDto> SubjectSummaries
);