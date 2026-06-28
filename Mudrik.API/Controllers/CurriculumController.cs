using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mudrik.Application.Services.Curriculum.Commands.CreateLesson;
using Mudrik.Application.Services.Curriculum.Commands.DeleteLesson;
using Mudrik.Application.Services.Curriculum.Commands.ImportLessons;
using Mudrik.Application.Services.Curriculum.Commands.ToggleLessonStatus;
using Mudrik.Application.Services.Curriculum.Commands.UpdateLesson;
using Mudrik.Application.Services.Curriculum.Queries.GetCurriculumStats;
using Mudrik.Application.Services.Curriculum.Queries.GetLessonById;
using Mudrik.Application.Services.Curriculum.Queries.GetLessons;
using Mudrik.Application.Services.Curriculum.Queries.GetLessonsBySubject;
using Mudrik.Application.Services.Curriculum.Queries.SearchLessons;
using System;

namespace Mudrik.API.Controllers
{
    [Route("api/curriculum")]
    [ApiController]
    [Authorize]
    public class CurriculumController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// KPI cards + subject tab strip at the top of the curriculum management page.
        /// GET /api/curriculum/stats
        /// </summary>
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var result = await mediator.Send(new GetCurriculumStatsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Paginated + filtered lessons table.
        /// GET /api/curriculum/lessons?subjectId=&gradeLevel=&isActive=&page=1&pageSize=20
        /// </summary>
        [HttpGet("lessons")]
        public async Task<IActionResult> GetLessons([FromQuery] GetLessonsQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Subject tab click — returns all active lessons for a subject.
        /// GET /api/curriculum/subjects/{subjectId}/lessons
        /// </summary>
        [HttpGet("subjects/{subjectId:guid}/lessons")]
        public async Task<IActionResult> GetLessonsBySubject([FromRoute] Guid subjectId)
        {
            var result = await mediator.Send(new GetLessonsBySubjectQuery(subjectId));
            return Ok(result);
        }

        /// <summary>
        /// Lesson detail — view/edit slide-over drawer.
        /// GET /api/curriculum/lessons/{id}
        /// </summary>
        [HttpGet("lessons/{id:guid}")]
        public async Task<IActionResult> GetLessonById([FromRoute] Guid id)
        {
            var result = await mediator.Send(new GetLessonByIdQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// Search box in the curriculum table toolbar.
        /// GET /api/curriculum/lessons/search?term=
        /// </summary>
        [HttpGet("lessons/search")]
        public async Task<IActionResult> SearchLessons([FromQuery] string term)
        {
            var result = await mediator.Send(new SearchLessonsQuery(term));
            return Ok(result);
        }

        /// <summary>
        /// "إضافة درس جديد" — add lesson slide-over form.
        /// POST /api/curriculum/lessons
        /// </summary>
        [HttpPost("lessons")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateLesson([FromBody] CreateLessonCommand command)
        {
            var lessonId = await mediator.Send(command);
            return CreatedAtAction(
                nameof(GetLessonById),
                new { id = lessonId },
                new { id = lessonId });
        }

        /// <summary>
        /// "تعديل بيانات الدرس" — edit lesson in slide-over form.
        /// PUT /api/curriculum/lessons/{id}
        /// </summary>
        [HttpPut("lessons/{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateLesson(
            [FromRoute] Guid id,
            [FromBody] UpdateLessonCommand command)
        {
            if (id != command.LessonId)
                return BadRequest("معرف الدرس في الـ URL لا يطابق المعرف في الـ body.");

            await mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Status toggle switch in the lesson form panel (نشط ↔ مسودة).
        /// PATCH /api/curriculum/lessons/{id}/toggle-status
        /// Returns the new IsActive value.
        /// </summary>
        [HttpPatch("lessons/{id:guid}/toggle-status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleStatus([FromRoute] Guid id)
        {
            var newStatus = await mediator.Send(new ToggleLessonStatusCommand(id));
            return Ok(new { isActive = newStatus });
        }

        /// <summary>
        /// "أرشفة الدرس" — soft delete (IsActive = false).
        /// DELETE /api/curriculum/lessons/{id}
        /// </summary>
        [HttpDelete("lessons/{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteLesson([FromRoute] Guid id)
        {
            await mediator.Send(new DeleteLessonCommand(id));
            return NoContent();
        }

        /// <summary>
        /// "استيراد" — bulk import up to 500 lessons.
        /// POST /api/curriculum/lessons/import
        /// </summary>
        [HttpPost("lessons/import")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ImportLessons([FromBody] ImportLessonsCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
