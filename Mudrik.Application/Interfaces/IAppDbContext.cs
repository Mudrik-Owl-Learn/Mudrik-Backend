using Microsoft.EntityFrameworkCore;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<ParentProfile> ParentProfiles { get; set; }
        DbSet<StudentProfile> StudentProfiles { get; set; }
        DbSet<LearnerAIProfile> LearnerAIProfiles { get; set; }
        DbSet<Subject> Subjects { get; set; }
        DbSet<StandardLesson> StandardLessons { get; set; }
        DbSet<QuizQuestion> QuizQuestions { get; set; }
        DbSet<AdaptedLesson> AdaptedLessons { get; set; }
        DbSet<LessonMicroChunk> LessonMicroChunks { get ; set; }
        DbSet<StudentLessonState> StudentLessonStates { get; set; }
        DbSet<AgentGeneratedQuiz> AgentGeneratedQuizzes { get; set; }
        DbSet<StudentQuizAnswer> StudentQuizAnswers { get; set; }
        DbSet<GamificationStreak> GamificationStreaks { get; set; }
        DbSet<XpTransaction> XpTransactions { get; set; }
        DbSet<Badge> Badges { get; set; }
        DbSet<StudentBadge> StudentBadges { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
