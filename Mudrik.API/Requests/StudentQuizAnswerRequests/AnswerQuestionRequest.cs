namespace Mudrik.API.Requests.StudentQuizAnswerRequests
{
    public class AnswerQuestionRequest
    {
        public Guid AgentGeneratedQuizId { get; set; }
        public Guid StudentProfileId { get; set; }
        public Guid QuizQuestionId { get; set; }
        public string SelectedOptionId { get; set; }
        //public bool IsCorrect { get; set; }
        public int TimeToAnswerMs { get; set; }
        public int AnswerChangeCount { get; set; }
    }
}
