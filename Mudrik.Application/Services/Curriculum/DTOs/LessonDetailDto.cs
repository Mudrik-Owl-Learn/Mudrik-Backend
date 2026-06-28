using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.DTOs;
/// <summary>
/// Full lesson detail — used in the view/edit slide-over drawer.
/// </summary>
public record LessonDetailDto(
    Guid Id,
    Guid SubjectId,
    string SubjectTitle,
    string SubjectIconUrl,
    int GradeLevel,
    string GradeLevelLabel,
    int ChapterNumber,
    int LessonOrder,
    string Title,
    string RawContentText,
    string LearningObjectivesJson,
    bool IsActive,
    DateTime CreatedAt
);