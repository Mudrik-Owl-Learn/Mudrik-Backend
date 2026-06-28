using MediatR;
using Mudrik.Application.Interfaces.CurriculumService;
using Mudrik.Application.Services.Curriculum.DTOs;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Commands.ImportLessons;
public class ImportLessonsCommandHandler(ICurriculumRepository repository)
       : IRequestHandler<ImportLessonsCommand, BulkImportResultDto>
{
    public async Task<BulkImportResultDto> Handle(
        ImportLessonsCommand request, CancellationToken cancellationToken)
    {
        int created = 0;
        int skipped = 0;
        var errors = new List<string>();

        foreach (var (item, index) in request.Lessons.Select((l, i) => (l, i)))
        {
            try
            {
                bool subjectExists = await repository.SubjectExistsAsync(item.SubjectId, cancellationToken);
                if (!subjectExists)
                {
                    errors.Add($"الصف {index + 1} ({item.Title}): المادة غير موجودة.");
                    continue;
                }

                bool exists = await repository.ExistsByTitleSubjectGradeAsync(
                    item.Title, item.SubjectId, item.GradeLevel, cancellationToken);

                if (exists) { skipped++; continue; }

                var lesson = StandardLesson.Create(
                    subjectId: item.SubjectId,
                    gradeLevel: item.GradeLevel,
                    chapterNumber: item.ChapterNumber,
                    lessonOrder: item.LessonOrder,
                    title: item.Title,
                    rawContentText: item.RawContentText,
                    learningObjectivesJson: item.LearningObjectivesJson,
                    isActive: item.IsActive);

                await repository.AddAsync(lesson, cancellationToken);
                created++;
            }
            catch (Exception ex)
            {
                errors.Add($"الصف {index + 1} ({item.Title}): {ex.Message}");
            }
        }

        await repository.SaveChangesAsync(cancellationToken);

        return new BulkImportResultDto(
            TotalProcessed: request.Lessons.Count,
            Created: created,
            Skipped: skipped,
            Errors: errors.AsReadOnly());
    }
}