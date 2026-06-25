using MediatR;
using Mudrik.Application.Services.AgentGeneratedQuizz.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.Commands.Generatequiz
{
    public class GenerateQuizCommand : IRequest<GenerateQuizDTO>
    {
        public Guid StudentProfileId { get; set; }
        public Guid? LessonMicroChunkId { get; set; } // shoul be null ---****!!!!----
        public Guid StandardLessonId { get; set; }
        public Guid SubjectId { get; set; }
        public string ConceptTag { get; set; }
        public int GradeLevel { get; set; }
        public int TotalTimeSeconds { get; set; }

    }
}
