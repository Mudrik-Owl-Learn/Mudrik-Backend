using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.DTOs;

/// <summary>
/// Paginated list result for the lessons table.
/// </summary>
public record LessonListResultDto(
    IReadOnlyList<LessonRowDto> Items,
    int TotalCount,
    int Page,
    int PageSize,
    int TotalPages
);