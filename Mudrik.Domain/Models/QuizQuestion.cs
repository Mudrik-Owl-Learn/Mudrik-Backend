using System;
using System.Collections.Generic;

namespace Mudrik.Domain.Entities
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        public int StandardLessonId { get; set; }
        public int SubjectId { get; set; }
        public string QuestionText { get; set; }
        public string Format { get; set; }
        public string OptionsJson { get; set; }
        public string CorrectOptionId { get; set; }
        public string ConceptTag { get; set; }
        public int GradeLevel { get; set; }
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public StandardLesson? StandardLesson { get; set; }
        public Subject? Subject { get; set; }
        public ICollection<StudentQuizAnswer> StudentQuizAnswers { get; set; } = new List<StudentQuizAnswer>();
    }
}
