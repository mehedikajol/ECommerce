namespace ECommerce.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string messages)
        : base(messages)
    {
    }

    public NotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }

}
