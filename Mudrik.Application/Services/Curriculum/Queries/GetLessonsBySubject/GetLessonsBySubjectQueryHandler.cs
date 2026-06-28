using MediatR;
using Mudrik.Application.Interfaces.CurriculumService;
using Mudrik.Application.Services.Curriculum.DTOs;
using Mudrik.Application.Services.Curriculum.Queries.GetLessons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Queries.GetLessonsBySubject;
public class GetLessonsBySubjectQueryHandler(ICurriculumRepository repository)
      : IRequestHandler<GetLessonsBySubjectQuery, IReadOnlyList<LessonRowDto>>
{
    public async Task<IReadOnlyList<LessonRowDto>> Handle(
        GetLessonsBySubjectQuery request, CancellationToken cancellationToken)
    {
        bool exists = await repository.SubjectExistsAsync(request.SubjectId, cancellationToken);
        if (!exists)
            throw new KeyNotFoundException("المادة الدراسية غير موجودة.");

        var lessons = await repository.GetBySubjectAsync(request.SubjectId, cancellationToken);

        return lessons
            .Select(GetLessonsQueryHandler.MapToRow)
            .ToList()
            .AsReadOnly();
    }
}