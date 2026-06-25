using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentQuizAnswer.DTOs
{
    public class QuizAnswerDTO
    {

        public QuizAnswerDTO()
        {

        }
        public QuizAnswerDTO(Guid id, Guid agentGeneratedQuizId, Guid studentProfileId, Guid quizQuestionId, string selectedOptionId, bool isCorrect, int timeToAnswerMs, int answerChangeCount, DateTime answeredAt)
        {
            Id = id;
            AgentGeneratedQuizId = agentGeneratedQuizId;
            StudentProfileId = studentProfileId;
            QuizQuestionId = quizQuestionId;
            SelectedOptionId = selectedOptionId;
            IsCorrect = isCorrect;
            TimeToAnswerMs = timeToAnswerMs;
            AnswerChangeCount = answerChangeCount;
            AnsweredAt = answeredAt;
        }

        public Guid Id { get; set; }
        public Guid AgentGeneratedQuizId { get; set; }
        public Guid StudentProfileId { get; set; }
        public Guid QuizQuestionId { get; set; }
        public string SelectedOptionId { get; set; }
        public bool IsCorrect { get; set; }
        public int TimeToAnswerMs { get; set; }
        public int AnswerChangeCount { get; set; }
        public DateTime AnsweredAt { get; set; }

    }
}
