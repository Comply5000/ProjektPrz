using API.Common.Exceptions;

namespace API.Features.Comments.Exeptions;

public class CommentNotFoundExeption : BaseException
{
    public CommentNotFoundExeption() : base("Opinia nie istnieje")
    {
    }
}


