using FluentValidation;
using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Curriculum.DTOs;
using Mudrik.Application.Services.Curriculum.Queries.GetLessons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Curriculum.Queries.GetLessonsBySubject
{
    // ── Query ─────────────────────────────────────────────────────────────────
    /// <summary>
    /// Returns all active lessons for a given subject, ordered by GradeLevel → ChapterNumber → LessonOrder.
    /// Triggered when the admin clicks a subject tab.
    /// </summary>
    public record GetLessonsBySubjectQuery(Guid SubjectId) : IRequest<IReadOnlyList<LessonRowDto>>;

}
