using System;
using System.Collections.Generic;

namespace Mudrik.Domain.Models
{
    /// <summary>
    /// Maps exactly to the StandardLessons table in the ERD.
    /// GUID Id PK | GUID SubjectId FK | INT GradeLevel | INT ChapterNumber
    /// INT LessonOrder | NVARCHAR Title | NVARCHAR RawContentText
    /// NVARCHAR LearningObjectivesJson | NVARCHAR PrerequisiteIdsJson
    /// BIT IsActive | DATETIME2 CreatedAt
    /// </summary>
    public class StandardLesson
    {
        public Guid Id { get; private set; }
        // ── Taxonomy ──────────────────────────────────────────────────
        public Guid SubjectId { get; private set; }
        public int GradeLevel { get; private set; }
        public int LessonOrder { get; private set; }
        public int ChapterNumber { get; private set; }
        public string Title { get; private set; }

        // ── Raw Content (source for AI adaptation) ────────────────────
        public string RawContentText { get; private set; }
        /// <summary>
        /// JSON array of learning objectives, e.g. ["يتعرف على أجزاء النبات", "يدرك وظيفة الجذر"]
        /// </summary>
        public string LearningObjectivesJson { get; private set; }

        /// <summary>
        /// JSON array of prerequisite lesson IDs (int[]).
        /// Stored as a JSON column — no separate LessonPrerequisites table.
        /// e.g. "[1, 5, 12]"
        /// </summary>
        public string PrerequisiteIdsJson { get; private set; }

        // ── Publishing ─────────────────────────────────────────────────
        public bool IsActive { get; private set; }

        // ── Audit ──────────────────────────────────────────────────────
        public DateTime CreatedAt { get; private set; }

        // Navigation properties — مطابقة للـ ERD بالكامل
        public Subject? Subject { get; set; }
        public ICollection<QuizQuestion> QuizQuestions { get; private set; } = new List<QuizQuestion>();
        public ICollection<AdaptedLesson> AdaptedLessons { get; private set; } = new List<AdaptedLesson>();
        public ICollection<StudentLessonState> StudentLessonStates { get; private set; } = new List<StudentLessonState>();
        public ICollection<AgentGeneratedQuiz> AgentGeneratedQuizzes { get; private set; } = new List<AgentGeneratedQuiz>();

        private StandardLesson() { }

        public static StandardLesson Create(
            Guid subjectId,
            int gradeLevel,
            int chapterNumber,
            int lessonOrder,
            string title,
            string rawContentText,
            string learningObjectivesJson,
            bool isActive = false)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("عنوان الدرس مطلوب.", nameof(title));

            if (gradeLevel is < 1 or > 4)
                throw new ArgumentOutOfRangeException(nameof(gradeLevel), "الصف الدراسي يجب أن يكون بين 1 و 4.");

            if (subjectId == Guid.Empty)
                throw new ArgumentException("معرف المادة الدراسية غير صحيح.", nameof(subjectId));

            return new StandardLesson
            {
                Id = Guid.NewGuid(),
                SubjectId = subjectId,
                GradeLevel = gradeLevel,
                ChapterNumber = chapterNumber,
                LessonOrder = lessonOrder,
                Title = title.Trim(),
                RawContentText = rawContentText.Trim(),
                LearningObjectivesJson = string.IsNullOrWhiteSpace(learningObjectivesJson)
                    ? "[]" : learningObjectivesJson.Trim(),
                PrerequisiteIdsJson = "[]",
                IsActive = isActive,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Update(
            Guid subjectId,
            int gradeLevel,
            int chapterNumber,
            int lessonOrder,
            string title,
            string rawContentText,
            string learningObjectivesJson)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("عنوان الدرس مطلوب.", nameof(title));

            if (gradeLevel is < 1 or > 4)
                throw new ArgumentOutOfRangeException(nameof(gradeLevel), "الصف الدراسي يجب أن يكون بين 1 و 4.");

            SubjectId = subjectId;
            GradeLevel = gradeLevel;
            ChapterNumber = chapterNumber;
            LessonOrder = lessonOrder;
            Title = title.Trim();
            RawContentText = rawContentText.Trim();
            LearningObjectivesJson = string.IsNullOrWhiteSpace(learningObjectivesJson)
                ? "[]" : learningObjectivesJson.Trim();
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
        public void ToggleStatus() => IsActive = !IsActive;
    }
}
