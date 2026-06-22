using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mudrik.API.Requests.StudentProfileRequests;
using Mudrik.Application.Services.StudentProfile.Commands.AddStudentProfile;
using Mudrik.Application.Services.StudentProfile.Commands.DeleteStudentProfile;
using Mudrik.Application.Services.StudentProfile.Commands.UpdateStudentProfile;
using Mudrik.Application.Services.StudentProfile.DTOs;
using Mudrik.Application.Services.StudentProfile.Queries.GetAllStudentsProfiles;
using Mudrik.Application.Services.StudentProfile.Queries.GetStudentProfileById;

namespace Mudrik.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentProfileController : ControllerBase
    {
        private readonly IMediator mediator;

        public StudentProfileController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddStudentProfileRequest request)
        {

            var command = new AddStudentProfileCommand
            {
                ParentProfileId = request.ParentProfileId,
                FirstName = request.FirstName,
                Age = request.Age,
                Gender = request.Gender,
                GradeLevel = request.GradeLevel,
                AvatarId = request.AvatarId,
                HasDyslexia = request.HasDyslexia,
                HasADHD = request.HasADHD,
                InterestsJson = request.InterestsJson,
                LearningStylePref = request.LearningStylePref,
                PersonalityTag = request.PersonalityTag,
            };

            var id = await mediator.Send(command);
            return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { id }
                );
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var query = new GetStudentProfileByIdQuery(id);
            var profile = await mediator.Send(query);

            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        [HttpGet()]
        public async Task<ActionResult> GetAllStudentsProfiles()
        {
            var query = new GetAllStudentsProfilesQuery();
            var profiles = await mediator.Send(query);

            return Ok(profiles);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudentProfile(Guid id)
        {
            var query = new DeleteStudentProfileCommand(id);
            var result = await mediator.Send(query);
            if (result <= 0)
                return NotFound();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudentProfile(Guid id, [FromBody] UpdateStudentProfileRequest request)
        {
            var command = new UpdateStudentProfileCommand
            {
                id = id,
                FirstName = request.FirstName,
                Age = request.Age,
                Gender = request.Gender,
                GradeLevel = request.GradeLevel,
                AvatarId = request.AvatarId,
                HasDyslexia = request.HasDyslexia,
                HasADHD = request.HasADHD,
                FontPreference = request.FontPreference,
                ColorOverlay = request.ColorOverlay,
                AudioEnabled = request.AudioEnabled,
                InterestsJson = request.InterestsJson,
                LearningStylePref = request.LearningStylePref,
                PersonalityTag = request.PersonalityTag,
            };

            var result = await mediator.Send(command);

            if (result <= 0)
                return NotFound();

            return Ok();

        }
    }
}
