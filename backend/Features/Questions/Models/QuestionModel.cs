namespace API.Features.Questions.Models
{
    public class QuestionModel
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
