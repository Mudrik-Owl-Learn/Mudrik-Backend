using FluentValidation;
using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Curriculum.Commands.CreateLesson
{
    public record CreateLessonCommand(
        Guid SubjectId,
        int GradeLevel,
        int ChapterNumber,
        int LessonOrder,
        string Title,
        string RawContentText,
        string LearningObjectivesJson,
        bool IsActive
    ) : IRequest<Guid>;

}
