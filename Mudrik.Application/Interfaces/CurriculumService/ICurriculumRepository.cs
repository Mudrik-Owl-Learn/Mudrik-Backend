using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Interfaces.CurriculumService
{
    // ── Internal projection used by GetCurriculumStatsQuery ──────────────────
    public record SubjectRawStats(
        Guid SubjectId,
        string Title,
        string IconUrl,
        int DisplayOrder,
        int TotalLessons,
        int ActiveLessons
    );

    public record CurriculumRawStats(
        int TotalSubjects,
        int TotalLessons,
        int ActiveLessons,
        int InactiveLessons,
        int TotalGrades,
        IReadOnlyList<SubjectRawStats> BySubject
    );

    // ── Repository Contract ──────────────────────────────────────────────────
    public interface ICurriculumRepository
    {
        // ── Subject Reads ────────────────────────────────────────────────────
        Task<IReadOnlyList<Subject>> GetActiveSubjectsAsync(CancellationToken cancellationToken);
        Task<bool> SubjectExistsAsync(Guid subjectId, CancellationToken cancellationToken);

        // ── Lesson Reads ─────────────────────────────────────────────────────
        Task<StandardLesson?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<(IReadOnlyList<StandardLesson> Items, int TotalCount)> GetPagedAsync(
            Guid? subjectId,
            int? gradeLevel,
            bool? isActive,
            string? searchTerm,
            int page,
            int pageSize,
            CancellationToken cancellationToken);

        Task<IReadOnlyList<StandardLesson>> GetBySubjectAsync(
            Guid subjectId, CancellationToken cancellationToken);

        Task<bool> ExistsByTitleSubjectGradeAsync(
            string title,
            Guid subjectId,
            int gradeLevel,
            CancellationToken cancellationToken,
            Guid? excludeId = null);

        Task<CurriculumRawStats> GetStatsAsync(CancellationToken cancellationToken);

        // ── Lesson Writes ────────────────────────────────────────────────────
        Task AddAsync(StandardLesson lesson, CancellationToken cancellationToken);
        Task UpdateAsync(StandardLesson lesson, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
