using MediatR;
using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.StudentProfile.DTOs;


namespace Mudrik.Application.Services.StudentProfile.Queries.GetAllStudentsProfiles
{
    public class GetAllStudentsProfilesQueryHandler(IAppDbContext _context) : IRequestHandler<GetAllStudentsProfilesQuery, List<GetStudentDTO>>
    {
        public async Task<List<GetStudentDTO>> Handle(GetAllStudentsProfilesQuery request, CancellationToken cancellationToken)
        {
            var profilesDtos = await _context.StudentProfiles
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
           .ToListAsync(cancellationToken);

            return profilesDtos;
        }
    }
}
