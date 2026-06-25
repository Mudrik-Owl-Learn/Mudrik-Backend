using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.QuizQuestion.Commands.AddQuizQuestion
{
    public class AddQuizQuestionCommand : IRequest<Guid>
    {
        public Guid StandardLessonId { get; set; }
        public Guid SubjectId { get; set; }
        public string QuestionText { get; set; }
        public string Format { get; set; }
        public string OptionsJson { get; set; }
        public string CorrectOptionId { get; set; }
        public string ConceptTag { get; set; }
        public int GradeLevel { get; set; }
    }
}
