using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mudrik.Application.Services.LearnerProfile.Commands.CreateLearnerAIProfile;
using Mudrik.Application.Services.LearnerProfile.Commands.UpdateLearnerAIProfile;
using Mudrik.Application.Services.LearnerProfile.Queries.GetLearnerAIProfile;
using System;

namespace Mudrik.API.Controllers
{
    [Route("api/learner-profiles")]
    [ApiController]
    [Authorize]
    public class LearnerProfileController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Returns the current LearnerAIProfile for a student.
        /// Called by the Adaptation Agent before generating any lesson content.
        /// </summary>
        [HttpGet("{studentProfileId:guid}")]
        public async Task<IActionResult> GetProfile([FromRoute] Guid studentProfileId)
        {
            var result = await mediator.Send(new GetLearnerAIProfileQuery(studentProfileId));
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Creates initial LearnerAIProfile — called once from SubmitDiagnosticAnswersCommand.
        /// Returns 409 if a profile already exists for this student.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] CreateLearnerAIProfileCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetProfile),
                new { studentProfileId = result.StudentProfileId }, result);
        }

        /// <summary>
        /// Updates and re-versions the profile — called after lesson completion
        /// for recalibration. Always increments ProfileVersion.
        /// </summary>
        [HttpPut("{studentProfileId:guid}")]
        public async Task<IActionResult> UpdateProfile(
            [FromRoute] Guid studentProfileId,
            [FromBody] UpdateLearnerAIProfileCommand command)
        {
            if (studentProfileId != command.StudentProfileId)
                return BadRequest("Route studentProfileId does not match body.");

            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
