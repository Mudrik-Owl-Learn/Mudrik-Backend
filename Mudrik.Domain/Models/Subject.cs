using System.Collections.Generic;

namespace Mudrik.Domain.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string IconUrl { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public ICollection<StandardLesson> StandardLessons { get; set; } = new List<StandardLesson>();
        public ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();
    }
}
