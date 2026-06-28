using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.DTOs;
/// <summary>
/// Lightweight lesson row — used in the admin curriculum table.
/// LastUpdated is human-readable Arabic relative time.
/// </summary>
public record LessonRowDto(
    Guid Id,
    string Title,
    Guid SubjectId,
    string SubjectTitle,
    int GradeLevel,
    string GradeLevelLabel,
    int ChapterNumber,
    int LessonOrder,
    bool IsActive,
    string LastUpdated
);