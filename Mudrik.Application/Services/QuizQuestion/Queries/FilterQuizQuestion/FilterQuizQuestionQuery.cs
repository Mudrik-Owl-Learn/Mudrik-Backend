using MediatR;
using Mudrik.Application.Services.QuizQuestion.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.QuizQuestion.Queries.FilterQuizQuestion
{
    public class FilterQuizQuestionQuery : IRequest<List<GenerateQuizQuestionDTO>>
    {
        public Guid StandardLessonId { get; set; }
        public string ConceptTag { get; set; }
        public int GradeLevel { get; set; }
    }
}
