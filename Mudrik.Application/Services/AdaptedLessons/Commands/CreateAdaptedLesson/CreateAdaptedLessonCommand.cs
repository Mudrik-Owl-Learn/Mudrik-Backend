using MediatR;
using Mudrik.Application.Services.Lessons.DTOs;
using System;

namespace Mudrik.Application.Services.Lessons.Commands.CreateAdaptedLesson
{
    public record CreateAdaptedLessonCommand(
        Guid StudentProfileId,
        Guid StandardLessonId
    ) : IRequest<AdaptedLessonDto>;
}
