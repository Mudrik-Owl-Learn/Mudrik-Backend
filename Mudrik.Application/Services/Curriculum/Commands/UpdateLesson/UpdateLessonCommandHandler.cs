using MediatR;
using Mudrik.Application.Interfaces.CurriculumService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Commands.UpdateLesson;
public class UpdateLessonCommandHandler(ICurriculumRepository repository)
       : IRequestHandler<UpdateLessonCommand>
{
    public async Task Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        // 1. Load lesson
        var lesson = await repository.GetByIdAsync(request.LessonId, cancellationToken)
            ?? throw new KeyNotFoundException($"الدرس غير موجود.");

        // 2. Validate subject exists
        bool subjectExists = await repository.SubjectExistsAsync(request.SubjectId, cancellationToken);
        if (!subjectExists)
            throw new ValidationException("المادة الدراسية المختارة غير موجودة.");

        // 3. Guard: duplicate title (excluding current lesson)
        bool duplicate = await repository.ExistsByTitleSubjectGradeAsync(
            request.Title, request.SubjectId, request.GradeLevel,
            cancellationToken, excludeId: request.LessonId);

        if (duplicate)
            throw new InvalidOperationException("يوجد بالفعل درس بنفس العنوان في هذه المادة والصف.");

        // 4. Update fields
        lesson.Update(
            subjectId: request.SubjectId,
            gradeLevel: request.GradeLevel,
            chapterNumber: request.ChapterNumber,
            lessonOrder: request.LessonOrder,
            title: request.Title,
            rawContentText: request.RawContentText,
            learningObjectivesJson: request.LearningObjectivesJson);

        // 5. Sync IsActive
        if (request.IsActive && !lesson.IsActive) lesson.Activate();
        else if (!request.IsActive && lesson.IsActive) lesson.Deactivate();

        await repository.UpdateAsync(lesson, cancellationToken);
    }
}