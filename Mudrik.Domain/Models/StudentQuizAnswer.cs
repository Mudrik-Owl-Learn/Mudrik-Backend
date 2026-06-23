using System;

namespace Mudrik.Domain.Models
{
    public class StudentQuizAnswer
    {
        public Guid Id { get; set; }
        public Guid AgentGeneratedQuizId { get; set; }
        public Guid StudentProfileId { get; set; }
        public Guid QuizQuestionId { get; set; }
        public string SelectedOptionId { get; set; }
        public bool IsCorrect { get; set; }
        public int TimeToAnswerMs { get; set; }
        public int AnswerChangeCount { get; set; }
        public DateTime AnsweredAt { get; set; } = DateTime.Now;

        // Navigation properties
        public AgentGeneratedQuiz? AgentGeneratedQuiz { get; set; }
        public StudentProfile? StudentProfile { get; set; }
        public QuizQuestion? QuizQuestion { get; set; }
    }
}
