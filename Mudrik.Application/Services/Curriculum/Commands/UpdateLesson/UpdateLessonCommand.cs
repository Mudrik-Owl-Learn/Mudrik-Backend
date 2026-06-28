using FluentValidation;
using MediatR;
using Mudrik.Application.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Curriculum.Commands.UpdateLesson
{
    // ── Command ───────────────────────────────────────────────────────────────
    public record UpdateLessonCommand(
        Guid LessonId,
        Guid SubjectId,
        int GradeLevel,
        int ChapterNumber,
        int LessonOrder,
        string Title,
        string RawContentText,
        string LearningObjectivesJson,
        bool IsActive
    ) : IRequest;

   
}
