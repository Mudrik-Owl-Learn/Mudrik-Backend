using FluentValidation;
using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Curriculum.DTOs;
using Mudrik.Application.Services.Curriculum.Queries.GetLessons;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Curriculum.Queries.SearchLessons
{
    // ── Query ─────────────────────────────────────────────────────────────────
    /// <summary>
    /// Full-text search across lesson titles, chapters, and grade labels.
    /// Driven by the search box in the admin curriculum table toolbar.
    /// Returns up to 50 results ordered by relevance (title match first).
    /// </summary>
    public record SearchLessonsQuery(string Term) : IRequest<IReadOnlyList<LessonRowDto>>;
    
}
