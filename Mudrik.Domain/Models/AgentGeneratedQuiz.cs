using System;
using System.Collections.Generic;

namespace Mudrik.Domain.Entities
{
    public class AgentGeneratedQuiz
    {
        public Guid Id { get; set; }
        public Guid StudentProfileId { get; set; }
        public Guid LessonMicroChunkId { get; set; }
        public Guid StandardLessonId { get; set; }
        public int AttemptNumber { get; set; }
        public int AudioReplayCount { get; set; }
        public decimal ScorePercent { get; set; }
        public bool IsPassed { get; set; }
        public int TotalTimeSeconds { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; } 

        // Navigation properties
        public StudentProfile? StudentProfile { get; set; }
        public LessonMicroChunk? LessonMicroChunk { get; set; }
        public StandardLesson StandardLesson { get; set; }
        public ICollection<StudentQuizAnswer> StudentQuizAnswers { get; set; } = new List<StudentQuizAnswer>();
    }
}
