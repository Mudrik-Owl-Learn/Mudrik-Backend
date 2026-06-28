using MediatR;
using Mudrik.Application.Interfaces.CurriculumService;
using Mudrik.Application.Services.Curriculum.DTOs;
using Mudrik.Application.Services.Curriculum.Queries.GetLessons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Queries.SearchLessons;
public class SearchLessonsQueryHandler(ICurriculumRepository repository)
        : IRequestHandler<SearchLessonsQuery, IReadOnlyList<LessonRowDto>>
{
    public async Task<IReadOnlyList<LessonRowDto>> Handle(
        SearchLessonsQuery request, CancellationToken cancellationToken)
    {
        var (lessons, _) = await repository.GetPagedAsync(
            subjectId: null,
            gradeLevel: null,
            isActive: null,
            searchTerm: request.Term,
            page: 1,
            pageSize: 50,
            cancellationToken: cancellationToken);

        return lessons
            .Select(GetLessonsQueryHandler.MapToRow)
            .ToList()
            .AsReadOnly();
    }
}