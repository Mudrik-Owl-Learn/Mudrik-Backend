using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.DTOs
{
    public class CompleteQuizResultDTO
    {
        public Guid Id { get; set; }
        public decimal ScorePercent { get; set; }
        public bool IsPassed { get; set; }
        public int TotalTimeSeconds { get; set; }
        public int TotalXpAwarded { get; set; }
        public int CurrentStreak { get; set; }
        public List<Guid> NewlyEarnedBadgeIds { get; set; } = new();
    }
}
