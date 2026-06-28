using MediatR;
using Mudrik.Application.Interfaces.CurriculumService;
using Mudrik.Application.Services.Curriculum.DTOs;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Queries.GetLessons;

public class GetLessonsQueryHandler(ICurriculumRepository repository)
    : IRequestHandler<GetLessonsQuery, LessonListResultDto>
{
    public async Task<LessonListResultDto> Handle(
        GetLessonsQuery request, CancellationToken cancellationToken)
    {
        var (lessons, totalCount) = await repository.GetPagedAsync(
            subjectId: request.SubjectId,
            gradeLevel: request.GradeLevel,
            isActive: request.IsActive,
            searchTerm: request.SearchTerm,
            page: request.Page,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken);

        var rows = lessons.Select(MapToRow).ToList().AsReadOnly();

        return new LessonListResultDto(
            Items: rows,
            TotalCount: totalCount,
            Page: request.Page,
            PageSize: request.PageSize,
            TotalPages: (int)Math.Ceiling(totalCount / (double)request.PageSize));
    }

    internal static LessonRowDto MapToRow(StandardLesson l) => new(
        Id: l.Id,
        Title: l.Title,
        SubjectId: l.SubjectId,
        SubjectTitle: l.Subject?.Title ?? string.Empty,
        GradeLevel: l.GradeLevel,
        GradeLevelLabel: $"الصف {l.GradeLevel} الابتدائي",
        ChapterNumber: l.ChapterNumber,
        LessonOrder: l.LessonOrder,
        IsActive: l.IsActive,
        LastUpdated: FormatRelativeTime(l.CreatedAt));

    internal static string FormatRelativeTime(DateTime utcTime)
    {
        var diff = DateTime.UtcNow - utcTime;
        return diff.TotalMinutes < 60 ? $"منذ {(int)diff.TotalMinutes} دقيقة"
             : diff.TotalHours < 24 ? $"منذ {(int)diff.TotalHours} ساعة"
             : diff.TotalDays < 30 ? $"منذ {(int)diff.TotalDays} يوم"
             : diff.TotalDays < 365 ? $"منذ {(int)(diff.TotalDays / 30)} شهر"
                                       : $"منذ {(int)(diff.TotalDays / 365)} سنة";
    }
}