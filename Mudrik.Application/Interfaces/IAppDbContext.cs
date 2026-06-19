using Microsoft.EntityFrameworkCore;
using Mudrik.Domain.Entities;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<ApplicationUser> Users { get;}
        DbSet<ParentProfile> ParentProfiles { get;}
        DbSet<StudentProfile> StudentProfiles { get; }
        DbSet<LearnerAIProfile> LearnerAIProfiles { get; }
        DbSet<Subject> Subjects { get; }
        DbSet<StandardLesson> StandardLessons { get; }
        DbSet<QuizQuestion> QuizQuestions { get; }
        DbSet<AdaptedLesson> AdaptedLessons { get; }
        DbSet<LessonMicroChunk> LessonMicroChunks { get ; }
        DbSet<StudentLessonState> StudentLessonStates { get; }
        DbSet<AgentGeneratedQuiz> AgentGeneratedQuizzes { get; }
        DbSet<StudentQuizAnswer> StudentQuizAnswers { get; }
        DbSet<GamificationStreak> GamificationStreaks { get; }
        DbSet<XpTransaction> XpTransactions { get; }
        DbSet<Badge> Badges { get; }
        DbSet<StudentBadge> StudentBadges { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
