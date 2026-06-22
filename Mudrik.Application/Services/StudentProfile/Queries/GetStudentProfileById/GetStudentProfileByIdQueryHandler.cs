using MediatR;
using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.StudentProfile.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentProfile.Queries.GetStudentProfileById
{
    public class GetStudentProfileByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetStudentProfileByIdQuery, GetStudentDTO>
    {
        public async Task<GetStudentDTO?> Handle(
        GetStudentProfileByIdQuery request,
        CancellationToken cancellationToken)
        {
            return await context.StudentProfiles
                .Where(x => x.Id == request.id)
                .Select(x => new GetStudentDTO
                {
                    Id = x.Id,
                    ParentProfileId = x.ParentProfileId,
                    FirstName = x.FirstName,
                    Age = x.Age,
                    Gender = x.Gender,
                    GradeLevel = x.GradeLevel,
                    AvatarId = x.AvatarId,
                    HasDyslexia = x.HasDyslexia,
                    HasADHD = x.HasADHD,
                    FontPreference = x.FontPreference,
                    ColorOverlay = x.ColorOverlay,
                    AudioEnabled = x.AudioEnabled,
                    InterestsJson = x.InterestsJson,
                    LearningStylePref = x.LearningStylePref,
                    PersonalityTag = x.PersonalityTag
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
