using System;
using System.Collections.Generic;

namespace Mudrik.Domain.Entities
{
    public class StandardLesson
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int GradeLevel { get; set; }
        public int ChapterNumber { get; set; }
        public int LessonOrder { get; set; }
        public string Title { get; set; }
        public string RawContentText { get; set; }
        public string LearningObjectivesJson { get; set; }
        public string PrerequisiteIdsJson { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Subject? Subject { get; set; }
        public ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();
        public ICollection<AdaptedLesson> AdaptedLessons { get; set; } = new List<AdaptedLesson>();
        public ICollection<StudentLessonState> StudentLessonStates { get; set; } = new List<StudentLessonState>();
        public ICollection<AgentGeneratedQuiz> AgentGeneratedQuizzes { get; set; } = new List<AgentGeneratedQuiz>();
    }
}
