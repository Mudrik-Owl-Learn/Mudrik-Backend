using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using Mudrik.Domain.Configurations;
using Mudrik.Domain.Entities;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Mudrik.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options), IAppDbContext
    {
        public DbSet<ParentProfile> ParentProfiles { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }
        public DbSet<LearnerAIProfile> LearnerAIProfiles { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StandardLesson> StandardLessons { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<AdaptedLesson> AdaptedLessons { get; set; }
        public DbSet<LessonMicroChunk> LessonMicroChunks { get; set; }
        public DbSet<StudentLessonState> StudentLessonStates { get; set; }
        public DbSet<AgentGeneratedQuiz> AgentGeneratedQuizzes { get; set; }
        public DbSet<StudentQuizAnswer> StudentQuizAnswers { get; set; }
        public DbSet<GamificationStreak> GamificationStreaks { get; set; }
        public DbSet<XpTransaction> XpTransactions { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<StudentBadge> StudentBadges { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new ParentProfileConfiguration());
            modelBuilder.ApplyConfiguration(new StudentProfileConfiguration());
            modelBuilder.ApplyConfiguration(new LearnerAIProfileConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration(new StandardLessonConfiguration());
            modelBuilder.ApplyConfiguration(new QuizQuestionConfiguration());
            modelBuilder.ApplyConfiguration(new AdaptedLessonConfiguration());
            modelBuilder.ApplyConfiguration(new LessonMicroChunkConfiguration());
            modelBuilder.ApplyConfiguration(new StudentLessonStateConfiguration());
            modelBuilder.ApplyConfiguration(new AgentGeneratedQuizConfiguration());
            modelBuilder.ApplyConfiguration(new StudentQuizAnswerConfiguration());
            modelBuilder.ApplyConfiguration(new GamificationStreakConfiguration());
            modelBuilder.ApplyConfiguration(new XpTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new BadgeConfiguration());
            modelBuilder.ApplyConfiguration(new StudentBadgeConfiguration());
        }

    }
}
