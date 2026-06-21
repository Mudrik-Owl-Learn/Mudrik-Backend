using MediatR;
using Mudrik.Application.Interfaces;

namespace Mudrik.Application.Services.StudentProfile.Commands.UpdateStudentProfile;

public class UpdateStudentProfileCommandHandler(IAppDbContext _context) : IRequestHandler<UpdateStudentProfileCommand, int>
{
    public async Task<int> Handle(UpdateStudentProfileCommand request, CancellationToken cancellationToken)
    {
        var studentProfile = await _context.StudentProfiles
            .FindAsync(new object[] { request.id }, cancellationToken);

        if (studentProfile is null)
            return 0;

        studentProfile.FirstName = request.FirstName;
        studentProfile.Age = request.Age;
        studentProfile.Gender = request.Gender;
        studentProfile.GradeLevel = request.GradeLevel;
        studentProfile.AvatarId = request.AvatarId;
        studentProfile.HasDyslexia = request.HasDyslexia;
        studentProfile.HasADHD = request.HasADHD;
        studentProfile.FontPreference = request.FontPreference;
        studentProfile.ColorOverlay = request.ColorOverlay;
        studentProfile.AudioEnabled = request.AudioEnabled;
        studentProfile.InterestsJson = request.InterestsJson;
        studentProfile.LearningStylePref = request.LearningStylePref;
        studentProfile.PersonalityTag = request.PersonalityTag;

        return await _context.SaveChangesAsync(cancellationToken);
    }
}