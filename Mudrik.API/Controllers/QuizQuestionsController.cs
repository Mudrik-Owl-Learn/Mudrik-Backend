using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mudrik.API.Requests.QuizQuestionsRequests;
using Mudrik.Application.Services.QuizQuestion.Commands.AddQuizQuestion;
using Mudrik.Application.Services.QuizQuestion.Commands.DeleteQuizQuestion;
using Mudrik.Application.Services.QuizQuestion.Commands.UpdateQuizQuestion;
using Mudrik.Application.Services.QuizQuestion.DTOs;
using Mudrik.Application.Services.QuizQuestion.Queries.FilterQuizQuestion;
using Mudrik.Application.Services.QuizQuestion.Queries.GetQuizQuestionById;
using Mudrik.Application.Services.StudentProfile.Queries.GetStudentProfileById;

namespace Mudrik.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizQuestionsController(IMediator mediator) : ControllerBase
    {


        [HttpPost("GenerateQuizQuestions")]
        public async Task<ActionResult> GenerateQuizQuestionsManually(List<AddQuizQuestionCommand> addQuizQuestionCommands)
        {
            var ids = new List<Guid>();
            foreach (var command in addQuizQuestionCommands)
            {
                var id = await mediator.Send(command);
                ids.Add(id);
            }

            return Created();

        }

        // To Be Done *********************************************************
        //    public async Task<ActionResult> GenerateQuizQuestionAI(AddQuizQuestionCommand addQuizQuestionCommand)  ----> Ai Generate Questions
        // *********************************************************************

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var question = await mediator.Send(new GetQuizQuestionByIdQuery(id));
            if (question == null)
                return NotFound();

            return Ok(question);
        }

        [HttpGet("FilterQuestions")]
        public async Task<ActionResult> FilterQuestions([FromQuery] FilterQuestionRequest request)
        {
            FilterQuizQuestionQuery query = new FilterQuizQuestionQuery
            {
                StandardLessonId = request.StandardLessonId,
                ConceptTag = request.ConceptTag,
                GradeLevel = request.GradeLevel
            };
            var questions = await mediator.Send(query);
            if (questions == null)
                return NotFound();

            return Ok(questions);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] EditQuestionRequest request)
        {
            if (id != request.Id)
                return BadRequest("ID in the route does not match ID in the request body.");

            var result = await mediator.Send(new UpdateQuizQuestionCommand
            {
                Id = request.Id,
                StandardLessonId = request.StandardLessonId,
                SubjectId = request.SubjectId,
                QuestionText = request.QuestionText,
                Format = request.Format,
                OptionsJson = request.OptionsJson,
                CorrectOptionId = request.CorrectOptionId,
                ConceptTag = request.ConceptTag,
                GradeLevel = request.GradeLevel,
                GeneratedAt = request.GeneratedAt
            });


            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool result = await mediator.Send(new DeleteQuizQuestionCommand(id));
            if (!result)
                return NotFound();

            return NoContent();
        }

    }
}
