using FluentValidation;
using MediatR;
using Mudrik.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Curriculum.Commands.ToggleLessonStatus
{
    // ── Command ───────────────────────────────────────────────────────────────
    /// <summary>
    /// Toggles BIT IsActive on a lesson (true ↔ false).
    /// Used by the status switch in the lesson form panel.
    /// Returns the new IsActive value.
    /// </summary>
    public record ToggleLessonStatusCommand(Guid LessonId) : IRequest<bool>;

    
}
