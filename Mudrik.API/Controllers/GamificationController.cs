using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mudrik.Application.Services.Gamification.Commands.AwardXp;
using Mudrik.Application.Services.Gamification.Commands.GrantFreezeToken;
using Mudrik.Application.Services.Gamification.Commands.RecordDailyActivity;
using Mudrik.Application.Services.Gamification.Queries.GetLeaderboard;
using Mudrik.Application.Services.Gamification.Queries.GetStreakStatus;
using Mudrik.Application.Services.Gamification.Queries.GetXpHistory;
using Mudrik.Application.Services.Gamification.Queries.GetXpTransactionById;

namespace Mudrik.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class GamificationController(IMediator mediator) : ControllerBase
    {
        // ---- XpTransactions ----

        [HttpPost("xp/award")]
        public async Task<IActionResult> AwardXp([FromBody] AwardXpCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("xp/transaction/{id:guid}")]
        public async Task<IActionResult> GetXpTransactionById([FromRoute] Guid id)
        {
            var result = await mediator.Send(new GetXpTransactionByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("xp/{studentProfileId:guid}/history")]
        public async Task<IActionResult> GetXpHistory(
            [FromRoute] Guid studentProfileId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await mediator.Send(new GetXpHistoryQuery(studentProfileId, pageNumber, pageSize));
            return Ok(result);
        }

        // ---- GamificationStreaks ----

        [HttpPost("streaks/record-activity")]
        public async Task<IActionResult> RecordDailyActivity([FromBody] RecordDailyActivityCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("streaks/{studentProfileId:guid}")]
        public async Task<IActionResult> GetStreakStatus([FromRoute] Guid studentProfileId)
        {
            var result = await mediator.Send(new GetStreakStatusQuery(studentProfileId));
            return Ok(result);
        }

        [HttpPost("streaks/grant-freeze-token")]
        public async Task<IActionResult> GrantFreezeToken([FromBody] GrantFreezeTokenCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        // ---- Leaderboard (derived from GamificationStreaks) ----

        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboard([FromQuery] GetLeaderboardQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
