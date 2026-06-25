namespace Mudrik.API.Requests.StudentQuizAnswerRequests
{
    public class GetAnswersByAnyPropertyRequest
    {
        public Guid StudentProfileId { get; set; }
        public string? ConceptTag { get; set; }
        public Guid? StandardLessonId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public bool? IsCorrect { get; set; }
    }
}
