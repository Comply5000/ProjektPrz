namespace API.Features.Questions.Models
{
    public class QuestionModel
    {
        public string Message { get; set; }
        public string Answer { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? AnsweredAt { get; set; }

        public Guid Id { get; set; }
    }
}
