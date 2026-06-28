using MediatR;
using Mudrik.Application.Interfaces.CurriculumService;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Commands.CreateLesson;
public class CreateLessonCommandHandler(ICurriculumRepository repository)
        : IRequestHandler<CreateLessonCommand, Guid>
{
    public async Task<Guid> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        bool subjectExists = await repository.SubjectExistsAsync(request.SubjectId, cancellationToken);
        if (!subjectExists)
            throw new ValidationException("المادة الدراسية المختارة غير موجودة.");

        bool duplicate = await repository.ExistsByTitleSubjectGradeAsync(
            request.Title, request.SubjectId, request.GradeLevel, cancellationToken);

        if (duplicate)
            throw new InvalidOperationException("يوجد بالفعل درس بنفس العنوان في هذه المادة والصف.");

        var lesson = StandardLesson.Create(
            subjectId: request.SubjectId,
            gradeLevel: request.GradeLevel,
            chapterNumber: request.ChapterNumber,
            lessonOrder: request.LessonOrder,
            title: request.Title,
            rawContentText: request.RawContentText,
            learningObjectivesJson: request.LearningObjectivesJson,
            isActive: request.IsActive);

        await repository.AddAsync(lesson, cancellationToken);

        return lesson.Id;
    }
}