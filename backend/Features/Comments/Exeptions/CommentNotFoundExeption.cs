using API.Common.Exceptions;

namespace API.Features.Comments.Exeptions;

public class CommentNotFoundExeption : BaseException
{
    public CommentNotFoundExeption() : base("comment doesn't exist")
    {
    }
}


