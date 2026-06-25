using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.DTOs
{
    public class GenerateQuizDTO
    {
        public Guid id { get; set; }
        public List<QuizQuestionWithoutCorrectOptionId>? questions { get; set; }
    }
}
