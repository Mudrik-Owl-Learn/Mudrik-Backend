using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentQuizAnswer.Commands.AddQuestionAnswer
{
    public class AddQuestionAnswerCommand : IRequest<Guid>
    {
        public Guid AgentGeneratedQuizId { get; set; }
        public Guid StudentProfileId { get; set; }
        public Guid QuizQuestionId { get; set; }
        public string SelectedOptionId { get; set; }
        public int TimeToAnswerMs { get; set; }
        public int AnswerChangeCount { get; set; }
    }
}
