using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.StudentProfile.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentProfile.Commands.AddStudentProfile
{
    public class AddStudentProfileCommandHandler(IAppDbContext context) : IRequestHandler<AddStudentProfileCommand, int>
    {
        public async Task<int> Handle(AddStudentProfileCommand request, CancellationToken cancellationToken)
        {
            var studentProfile = new Domain.Entities.StudentProfile
            {
                UserId = "33",
                ParentProfileId = request.ParentProfileId,
                FirstName = request.FirstName,
                Age = request.Age,
                Gender = request.Gender,
                GradeLevel = request.GradeLevel,
                AvatarId = request.AvatarId,
                HasDyslexia = request.HasDyslexia,
                HasADHD = request.HasADHD,
                FontPreference = "Times New Roman",
                ColorOverlay = "Red",
                AudioEnabled = true,
                InterestsJson = request.InterestsJson,
                LearningStylePref = request.LearningStylePref,
                PersonalityTag = request.PersonalityTag,
                OnboardingComplete = true
            };


            await context.StudentProfiles.AddAsync(studentProfile, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return request.Id;
        }
    }
}
