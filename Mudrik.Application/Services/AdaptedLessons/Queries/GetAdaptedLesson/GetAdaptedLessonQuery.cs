using MediatR;
using Mudrik.Application.Services.Lessons.DTOs;
using System;

namespace Mudrik.Application.Services.Lessons.Queries.GetAdaptedLesson
{
    public record GetAdaptedLessonQuery(
        Guid StandardLessonId,
        Guid StudentProfileId
    ) : IRequest<AdaptedLessonDto>;
}
