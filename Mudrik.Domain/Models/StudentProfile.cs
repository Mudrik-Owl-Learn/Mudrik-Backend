using Mudrik.Domain.Enums;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;

namespace Mudrik.Domain.Entities
{
    public class StudentProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ParentProfileId { get; set; }
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
        public bool OnboardingComplete { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ApplicationUser? User { get; set; }
        public ParentProfile? ParentProfile { get; set; }
        public LearnerAIProfile? LearnerAIProfile { get; set; }
        public GamificationStreak? GamificationStreak { get; set; }
        public ICollection<AdaptedLesson> AdaptedLessons { get; set; } = new List<AdaptedLesson>();
        public ICollection<LessonMicroChunk> LessonMicroChunks { get; set; } = new List<LessonMicroChunk>();
        public ICollection<StudentLessonState> StudentLessonStates { get; set; } = new List<StudentLessonState>();
        public ICollection<AgentGeneratedQuiz> AgentGeneratedQuizzes { get; set; } = new List<AgentGeneratedQuiz>();
        public ICollection<StudentQuizAnswer> StudentQuizAnswers { get; set; } = new List<StudentQuizAnswer>();
        public ICollection<XpTransaction> XpTransactions { get; set; } = new List<XpTransaction>();
        public ICollection<StudentBadge> StudentBadges { get; set; } = new List<StudentBadge>();
    }
}
