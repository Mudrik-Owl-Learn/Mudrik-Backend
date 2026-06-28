using MediatR;
using Mudrik.Application.Interfaces.CurriculumService;
using Mudrik.Application.Services.Curriculum.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Queries.GetLessonById;
public class GetLessonByIdQueryHandler(ICurriculumRepository repository)
        : IRequestHandler<GetLessonByIdQuery, LessonDetailDto>
{
    public async Task<LessonDetailDto> Handle(
        GetLessonByIdQuery request, CancellationToken cancellationToken)
    {
        var lesson = await repository.GetByIdAsync(request.LessonId, cancellationToken)
            ?? throw new KeyNotFoundException("الدرس غير موجود.");

        return new LessonDetailDto(
            Id: lesson.Id,
            SubjectId: lesson.SubjectId,
            SubjectTitle: lesson.Subject?.Title ?? string.Empty,
            SubjectIconUrl: lesson.Subject?.IconUrl ?? string.Empty,
            GradeLevel: lesson.GradeLevel,
            GradeLevelLabel: $"الصف {lesson.GradeLevel} الابتدائي",
            ChapterNumber: lesson.ChapterNumber,
            LessonOrder: lesson.LessonOrder,
            Title: lesson.Title,
            RawContentText: lesson.RawContentText,
            LearningObjectivesJson: lesson.LearningObjectivesJson,
            IsActive: lesson.IsActive,
            CreatedAt: lesson.CreatedAt);
    }
}