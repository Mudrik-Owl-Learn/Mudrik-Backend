using System;

namespace Mudrik.Domain.Entities
{
    public class StudentQuizAnswer
    {
        public int Id { get; set; }
        public int AgentGeneratedQuizId { get; set; }
        public int StudentProfileId { get; set; }
        public int QuizQuestionId { get; set; }
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
