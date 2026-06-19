using System;
using System.Collections.Generic;

namespace Mudrik.Domain.Entities
{
    public class LessonMicroChunk
    {
        public int Id { get; set; }
        public int AdaptedLessonId { get; set; }
        public int StudentProfileId { get; set; }
        public int ChunkOrder { get; set; }
        public string Format { get; set; }
        public string Title { get; set; }
        public string ContentText { get; set; }
        public string AudioScriptUrl { get; set; }
        public string IllustrationUrl { get; set; }
        public int EstimatedDurationSeconds { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }

        // Navigation properties
        public AdaptedLesson AdaptedLesson { get; set; }
        public StudentProfile StudentProfile { get; set; }
        public ICollection<AgentGeneratedQuiz> AgentGeneratedQuizzes { get; set; } = new List<AgentGeneratedQuiz>();
    }
}
