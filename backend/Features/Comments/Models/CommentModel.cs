namespace API.Features.Comments.Models
{
    public class CommentModel
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
