using FluentValidation;
using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Curriculum.DTOs;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Curriculum.Commands.ImportLessons
{
    // ── Sub-types ─────────────────────────────────────────────────────────────
    public record ImportLessonItem(
        Guid SubjectId,
        int GradeLevel,
        int ChapterNumber,
        int LessonOrder,
        string Title,
        string RawContentText,
        string LearningObjectivesJson,
        bool IsActive
    );

    // ── Command ───────────────────────────────────────────────────────────────
    /// <summary>
    /// Bulk-imports up to 500 lessons.
    /// Duplicates (same Title + SubjectId + GradeLevel) are skipped silently.
    /// Returns a detailed import summary.
    /// </summary>
    public record ImportLessonsCommand(
        IReadOnlyList<ImportLessonItem> Lessons
    ) : IRequest<BulkImportResultDto>;

     
}
