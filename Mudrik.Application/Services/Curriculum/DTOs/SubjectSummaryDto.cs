using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.DTOs;
/// <summary>
/// Per-subject breakdown — used in the subject tab strip.
/// StatusSummary: "نشط" | "مسودة" | "مسودة جزئياً" | "لا يوجد دروس"
/// </summary>
public record SubjectSummaryDto(
    Guid SubjectId,
    string Title,
    string IconUrl,
    int DisplayOrder,
    int TotalLessons,
    int ActiveLessons,
    string StatusSummary
);