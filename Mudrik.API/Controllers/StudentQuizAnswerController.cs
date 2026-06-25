using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mudrik.API.Requests.StudentQuizAnswerRequests;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.QuizQuestion.Queries.GetQuizQuestionById;
using Mudrik.Application.Services.StudentQuizAnswer.Commands.AddQuestionAnswer;
using Mudrik.Application.Services.StudentQuizAnswer.DTOs;
using Mudrik.Application.Services.StudentQuizAnswer.Queries.GetAnswersByAnyProperty;
using Mudrik.Application.Services.StudentQuizAnswer.Queries.GetQuestionAnswerById;
using Mudrik.Application.Services.StudentQuizAnswer.Queries.GetQuestionAnswerByQuizId;
using Mudrik.Application.Services.StudentQuizAnswer.Queries.GetQuestionAnswerByStudentId;
using Mudrik.Application.Services.StudentQuizAnswer.Queries.GetQuestionAnswersByConceptTag;
using Mudrik.Application.Services.StudentQuizAnswer.Queries.GetQuestionAnswersByQuestionId;
using System.Linq;

namespace Mudrik.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentQuizAnswerController(IMediator _mediator, IAppDbContext _context) : ControllerBase
    {
        [HttpPost("AnswerQuestion/{agentGeneratedQuizId}")]
        public async Task<ActionResult> AnswerQuestion(Guid agentGeneratedQuizId, [FromBody] AnswerQuestionRequest request)
        {
            var command = new AddQuestionAnswerCommand
            {
                AgentGeneratedQuizId = agentGeneratedQuizId,
                QuizQuestionId = request.QuizQuestionId,
                SelectedOptionId = request.SelectedOptionId,
                TimeToAnswerMs = request.TimeToAnswerMs,
                AnswerChangeCount = request.AnswerChangeCount
                // StudentProfileId intentionally absent — resolved server-side in the handler from the quiz
            };

            var answerId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetQuestionAnswerById), new { id = answerId }, new { id = answerId });
        }

        [HttpGet("GetByAnswerId/{id}")]
        public async Task<ActionResult<QuizAnswerDTO>> GetQuestionAnswerById(Guid id)
        {
            var query = new GetQuestionAnswerByIdQuery(id);
            var answer = await _mediator.Send(query);
            if (answer == null)
                return NotFound();

            return Ok(answer);
        }

        [HttpGet("GetByQuizId/{id}")]
        public async Task<ActionResult<List<QuizAnswerDTO>>> GetQuestionAnswerByQuizId(Guid id)
        {
            var query = new GetQuestionAnswerByQuizIdQuery(id);
            var answers = await _mediator.Send(query);
            return Ok(answers ?? new List<QuizAnswerDTO>());
        }

        [HttpGet("GetByStudentId/{id}")]
        public async Task<ActionResult<List<QuizAnswerDTO>>> GetQuestionAnswerByStudentId(Guid id)
        {
            var query = new GetQuestionAnswersByStudentIdQuery(id);
            var answers = await _mediator.Send(query);
            return Ok(answers ?? new List<QuizAnswerDTO>());
        }

        [HttpGet("GetByQuestionId/{id}")]
        public async Task<ActionResult<List<QuizAnswerDTO>>> GetQuestionAnswerByQuestionId(Guid id)
        {
            var query = new GetQuestionAnswersByQuestionIdQuery(id);
            var answers = await _mediator.Send(query);
            return Ok(answers ?? new List<QuizAnswerDTO>());
        }

        [HttpGet("GetByConceptTag/{conceptTag}")]
        public async Task<ActionResult<List<QuizAnswerDTO>>> GetQuestionAnswerByConceptTag(string conceptTag)
        {
            var query = new GetQuestionAnswersByConceptTagQuery(conceptTag);
            var answers = await _mediator.Send(query);
            if (answers == null || answers.Count <= 0)
                return NotFound();

            return Ok(answers);
        }

        [HttpGet("GetByAnyProperty")]
        public async Task<ActionResult<List<QuizAnswerDTO>>> GetQuestionAnswerByConceptTag([FromQuery] GetAnswersByAnyPropertyRequestQuery request)
        {

            var query = new GetAnswersByAnyPropertyRequestQuery
            {
                StudentProfileId = request.StudentProfileId,
                ConceptTag = request.ConceptTag,
                StandardLessonId = request.StandardLessonId,
                From = request.From,
                To = request.To,
                IsCorrect = request.IsCorrect
            };


            var answers = await _mediator.Send(query);
            if (answers == null || answers.Count <= 0)
                return NotFound();

            return Ok(answers);
        }
    }
}