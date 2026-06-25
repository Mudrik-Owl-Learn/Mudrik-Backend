using MediatR;
using Mudrik.Application.Services.StudentQuizAnswer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentQuizAnswer.Queries.GetAnswersByAnyProperty
{
    public class GetAnswersByAnyPropertyRequestQuery : IRequest<List<QuizAnswerDTO>>
    {
        public Guid StudentProfileId { get; set; }
        public string? ConceptTag { get; set; }
        public Guid? StandardLessonId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public bool? IsCorrect { get; set; }
    }
}
