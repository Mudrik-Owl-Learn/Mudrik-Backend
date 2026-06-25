using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Domain.Models
{
    public class AgentGeneratedQuizQuestion
    {
        public int Id { get; set; }
        public Guid AgentGeneratedQuizId { get; set; }
        public Guid QuizQuestionId { get; set; }

        // Navigation properties
        public AgentGeneratedQuiz AgentGeneratedQuiz { get; set; }
        public QuizQuestion QuizQuestion { get; set; }
    }
}
