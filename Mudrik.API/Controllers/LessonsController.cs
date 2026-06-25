using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mudrik.Application.Services.Lessons.Commands.CreateAdaptedLesson;
using Mudrik.Application.Services.Lessons.Queries.GetAdaptedLesson;
using System;

namespace Mudrik.API.Controllers
{
    [Route("api/lessons")]
    [ApiController]
    [Authorize]
    public class LessonsController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Returns the adapted lesson for the given student.
        /// If no adapted version exists yet, triggers generation automatically
        /// and returns the freshly generated result — never a 404.
        /// </summary>
        [HttpGet("{lessonId:guid}/adapted")]
        public async Task<IActionResult> GetAdaptedLesson(
            [FromRoute] Guid lessonId,
            [FromQuery] Guid studentId)
        {
            var result = await mediator.Send(
                new GetAdaptedLessonQuery(lessonId, studentId));
            return Ok(result);
        }

        /// <summary>
        /// Manually triggers adapted lesson generation.
        /// Useful for pre-warming the cache or forcing regeneration
        /// after a LearnerAIProfile update.
        /// </summary>
        [HttpPost("{lessonId:guid}/generate")]
        public async Task<IActionResult> GenerateAdaptedLesson(
            [FromRoute] Guid lessonId,
            [FromQuery] Guid studentId)
        {
            var result = await mediator.Send(
                new CreateAdaptedLessonCommand(studentId, lessonId));
            return Ok(result);
        }
    }
}
