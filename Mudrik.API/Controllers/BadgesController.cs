using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mudrik.Application.Services.Badges.Commands.CheckAndAwardBadges;
using Mudrik.Application.Services.Badges.Commands.MarkBadgeDisplayed;
using Mudrik.Application.Services.Badges.Queries.GetAllBadges;
using Mudrik.Application.Services.Badges.Queries.GetStudentBadges;
using System;

namespace Mudrik.API.Controllers
{
    [Route("api")]
    [ApiController]
    //[Authorize]
    public class BadgesController(IMediator mediator) : ControllerBase
    {
        /// <summary>Admin catalog — all active badge definitions.</summary>
        [HttpGet("badges")]
        public async Task<IActionResult> GetAllBadges()
        {
            var result = await mediator.Send(new GetAllBadgesQuery());
            return Ok(result);
        }

        /// <summary>Earned + locked split for Badge Shelf (/learn/profile) and Rewards Board (/learn/rewards).</summary>
        [HttpGet("students/{studentProfileId:guid}/badges")]
        public async Task<IActionResult> GetStudentBadges([FromRoute] Guid studentProfileId)
        {
            var result = await mediator.Send(new GetStudentBadgesQuery(studentProfileId));
            return Ok(result);
        }

        /// <summary>
        /// Called by the frontend after playing the badge unlock animation.
        /// Sets HasBeenDisplayed = true so the badge shelf knows not to re-animate it.
        /// </summary>
        [HttpPut("students/{studentProfileId:guid}/badges/{badgeId:guid}/mark-displayed")]
        public async Task<IActionResult> MarkDisplayed(
            [FromRoute] Guid studentProfileId,
            [FromRoute] Guid badgeId)
        {
            await mediator.Send(new MarkBadgeDisplayedCommand(studentProfileId, badgeId));
            return NoContent();
        }
    }
}
