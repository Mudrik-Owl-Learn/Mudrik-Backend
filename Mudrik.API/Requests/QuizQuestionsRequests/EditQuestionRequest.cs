namespace Mudrik.API.Requests.QuizQuestionsRequests
{
    public class EditQuestionRequest
    {
        public Guid Id { get; set; }
        public Guid StandardLessonId { get; set; }
        public Guid SubjectId { get; set; }
        public string QuestionText { get; set; }
        public string Format { get; set; }
        public string OptionsJson { get; set; }
        public string CorrectOptionId { get; set; }
        public string ConceptTag { get; set; }
        public int GradeLevel { get; set; }
        public DateTime GeneratedAt { get; set; }

    }
}
