using FluentValidation;
using MediatR;
using Mudrik.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Curriculum.Commands.DeleteLesson
{
    // ── Command ───────────────────────────────────────────────────────────────
    /// <summary>
    /// Soft-deletes a lesson by setting IsActive = false.
    /// Matches the "أرشفة الدرس" button in the view drawer.
    /// </summary>
    public record DeleteLessonCommand(Guid LessonId) : IRequest;

}
