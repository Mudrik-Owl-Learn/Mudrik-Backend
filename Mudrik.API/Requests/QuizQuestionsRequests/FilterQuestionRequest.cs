namespace Mudrik.API.Requests.QuizQuestionsRequests
{
    public class FilterQuestionRequest
    {
        public Guid StandardLessonId { get; set; }
        public string ConceptTag { get; set; }
        public int GradeLevel { get; set; }
    }
}
