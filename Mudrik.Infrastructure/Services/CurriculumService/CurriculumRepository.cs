using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Interfaces.CurriculumService;
using Mudrik.Domain.Models;
using Mudrik.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Infrastructure.Repositories
{
    public class CurriculumRepository(AppDbContext db) : ICurriculumRepository
    {
        // ── Subject Reads ────────────────────────────────────────────────────

        public async Task<IReadOnlyList<Subject>> GetActiveSubjectsAsync(
            CancellationToken cancellationToken)
        {
            var results = await db.Subjects
                .AsNoTracking()
                .Where(s => s.IsActive)
                .OrderBy(s => s.DisplayOrder)
                .ToListAsync(cancellationToken);

            return results.AsReadOnly();
        }

        public async Task<bool> SubjectExistsAsync(
            Guid subjectId, CancellationToken cancellationToken)
            => await db.Subjects.AnyAsync(
                s => s.Id == subjectId && s.IsActive, cancellationToken);

        // ── Lesson Reads ─────────────────────────────────────────────────────

        public async Task<StandardLesson?> GetByIdAsync(
            Guid id, CancellationToken cancellationToken)
            => await db.StandardLessons
                .Include(l => l.Subject)
                .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);

        public async Task<(IReadOnlyList<StandardLesson> Items, int TotalCount)> GetPagedAsync(
            Guid? subjectId,
            int? gradeLevel,
            bool? isActive,
            string? searchTerm,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var query = db.StandardLessons
                .AsNoTracking()
                .Include(l => l.Subject)
                .AsQueryable();

            if (subjectId.HasValue)
                query = query.Where(l => l.SubjectId == subjectId.Value);

            if (gradeLevel.HasValue)
                query = query.Where(l => l.GradeLevel == gradeLevel.Value);

            if (isActive.HasValue)
                query = query.Where(l => l.IsActive == isActive.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim().ToLower();
                query = query.Where(l =>
                    l.Title.ToLower().Contains(term) ||
                    EF.Functions.Like(l.Title, $"%{term}%"));
            }

            int totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(l => l.SubjectId)
                .ThenBy(l => l.GradeLevel)
                .ThenBy(l => l.ChapterNumber)
                .ThenBy(l => l.LessonOrder)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items.AsReadOnly(), totalCount);
        }

        public async Task<IReadOnlyList<StandardLesson>> GetBySubjectAsync(
            Guid subjectId, CancellationToken cancellationToken)
        {
            var results = await db.StandardLessons
                .AsNoTracking()
                .Include(l => l.Subject)
                .Where(l => l.SubjectId == subjectId && l.IsActive)
                .OrderBy(l => l.GradeLevel)
                .ThenBy(l => l.ChapterNumber)
                .ThenBy(l => l.LessonOrder)
                .ToListAsync(cancellationToken);

            return results.AsReadOnly();
        }

        public async Task<bool> ExistsByTitleSubjectGradeAsync(
            string title,
            Guid subjectId,
            int gradeLevel,
            CancellationToken cancellationToken,
            Guid? excludeId = null)
        {
            var query = db.StandardLessons
                .Where(l => l.Title == title.Trim() &&
                            l.SubjectId == subjectId &&
                            l.GradeLevel == gradeLevel);

            if (excludeId.HasValue)
                query = query.Where(l => l.Id != excludeId.Value);

            return await query.AnyAsync(cancellationToken);
        }

        public async Task<CurriculumRawStats> GetStatsAsync(CancellationToken cancellationToken)
        {
            // Single round-trip: subjects joined with their lesson counts
            var subjects = await db.Subjects
                .AsNoTracking()
                .Where(s => s.IsActive)
                .OrderBy(s => s.DisplayOrder)
                .Select(s => new
                {
                    s.Id,
                    s.Title,
                    s.IconUrl,
                    s.DisplayOrder,
                    TotalLessons  = s.StandardLessons.Count,
                    ActiveLessons = s.StandardLessons.Count(l => l.IsActive)
                })
                .ToListAsync(cancellationToken);

            var bySubject = subjects
                .Select(s => new SubjectRawStats(
                    SubjectId:     s.Id,
                    Title:         s.Title,
                    IconUrl:       s.IconUrl,
                    DisplayOrder:  s.DisplayOrder,
                    TotalLessons:  s.TotalLessons,
                    ActiveLessons: s.ActiveLessons))
                .ToList()
                .AsReadOnly();

            var lessonCounts = await db.StandardLessons
                .GroupBy(l => l.IsActive)
                .Select(g => new { IsActive = g.Key, Count = g.Count() })
                .ToListAsync(cancellationToken);

            int active   = lessonCounts.FirstOrDefault(x => x.IsActive)?.Count  ?? 0;
            int inactive = lessonCounts.FirstOrDefault(x => !x.IsActive)?.Count ?? 0;

            int totalGrades = await db.StandardLessons
                .Select(l => l.GradeLevel)
                .Distinct()
                .CountAsync(cancellationToken);

            return new CurriculumRawStats(
                TotalSubjects:   subjects.Count,
                TotalLessons:    active + inactive,
                ActiveLessons:   active,
                InactiveLessons: inactive,
                TotalGrades:     totalGrades,
                BySubject:       bySubject);
        }

        // ── Lesson Writes ────────────────────────────────────────────────────

        public async Task AddAsync(StandardLesson lesson, CancellationToken cancellationToken)
        {
            await db.StandardLessons.AddAsync(lesson, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(StandardLesson lesson, CancellationToken cancellationToken)
        {
            db.StandardLessons.Update(lesson);
            await db.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
            => await db.SaveChangesAsync(cancellationToken);
    }
}
