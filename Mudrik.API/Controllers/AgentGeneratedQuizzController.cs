using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mudrik.Application.Services.AgentGeneratedQuizz.Commands.CompleteQuiz;
using Mudrik.Application.Services.AgentGeneratedQuizz.Commands.Generatequiz;
using Mudrik.Application.Services.AgentGeneratedQuizz.Commands.IncrementAudioReplayCount;
using Mudrik.Application.Services.AgentGeneratedQuizz.Queries.GetQuizById;
using Mudrik.Application.Services.AgentGeneratedQuizz.Queries.GetQuizzesByStudent;

namespace Mudrik.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentGeneratedQuizzController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("generateQuiz")]
        public async Task<IActionResult> GenerateQuiz([FromBody] GenerateQuizCommand request)
        {
            // Validate the request
            if (request == null)
                return BadRequest("Invalid request.");

            var quiz = await _mediator.Send(request);

            return Ok(quiz);
        }

        [HttpPatch("IncrementAudioReplayCount/{id}")]
        public async Task<IActionResult> IncrementAudioReplayCount(Guid id)
        {
            var count = await _mediator.Send(new IncrementAudioReplayCountCommand(id));
            if (count == 0)
                return NotFound();

            return Ok($"Curent audio Replay Count is {count}");
        }

        [HttpGet("GetQuizById/{id}")]
        public async Task<IActionResult> GetQuizById(Guid id)
        {
            var quiz = await _mediator.Send(new GetQuizByIdQuery(id));
            if (quiz == null)
                return NotFound();

            return Ok(quiz);
        }

        [HttpGet("GetQuizzesByStudent/{id}")]
        public async Task<IActionResult> GetQuizzesByStudent(Guid id)
        {
            var quiz = await _mediator.Send(new GetQuizzesByStudentQuery(id));
            if (quiz == null)
                return NotFound();

            return Ok(quiz);
        }

        [HttpPost("CompleteQuiz/{id}")]
        public async Task<IActionResult> CompleteQuiz(Guid id)
        {
            var completeQuiz = await _mediator.Send(new CompleteQuizCommand(id));
            if (completeQuiz == null)
                return NotFound();

            return Ok(completeQuiz);
        }


    }
}
