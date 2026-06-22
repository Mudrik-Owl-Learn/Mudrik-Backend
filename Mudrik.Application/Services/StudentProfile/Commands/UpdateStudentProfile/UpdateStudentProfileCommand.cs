using MediatR;
using Mudrik.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentProfile.Commands.UpdateStudentProfile
{
    public class UpdateStudentProfileCommand : IRequest<int>
    {
        public Guid id { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public int GradeLevel { get; set; }
        public string AvatarId { get; set; }
        public bool HasDyslexia { get; set; }
        public bool HasADHD { get; set; }
        public string FontPreference { get; set; }
        public string ColorOverlay { get; set; }
        public bool AudioEnabled { get; set; }
        public string InterestsJson { get; set; }
        public string LearningStylePref { get; set; }
        public string PersonalityTag { get; set; }
    }
}
