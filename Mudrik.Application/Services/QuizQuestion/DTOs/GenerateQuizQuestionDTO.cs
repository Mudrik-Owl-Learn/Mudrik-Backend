using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.QuizQuestion.DTOs
{
    public class GenerateQuizQuestionDTO
    {
        public Guid Id { get; set; }
        public Guid StandardLessonId { get; set; }
        public Guid SubjectId { get; set; }
        public string QuestionText { get; set; }
        public string Format { get; set; }
        public string OptionsJson { get; set; }
        public string CorrectOptionId { get; set; }
        public string ConceptTag { get; set; }
        public int GradeLevel { get; set; }
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    }
}
