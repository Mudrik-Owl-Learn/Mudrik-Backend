using MediatR;
using Mudrik.Application.Services.StudentProfile.DTOs;
using Mudrik.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentProfile.Commands.AddStudentProfile
{
    public class AddStudentProfileCommand : IRequest<Guid>
    {
        public Guid ParentProfileId { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public int GradeLevel { get; set; }
        public string AvatarId { get; set; }
        public bool HasDyslexia { get; set; }
        public bool HasADHD { get; set; }
        //public string FontPreference { get; set; }
        //public string ColorOverlay { get; set; }
        //public bool AudioEnabled { get; set; }
        public string InterestsJson { get; set; }
        public string LearningStylePref { get; set; }
        public string PersonalityTag { get; set; }
        //public bool OnboardingComplete { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
