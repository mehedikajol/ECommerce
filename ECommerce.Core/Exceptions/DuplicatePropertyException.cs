namespace ECommerce.Core.Exceptions;

public class DuplicatePropertyException : Exception
{
    public DuplicatePropertyException()
    {
    }

    public DuplicatePropertyException( string message)
        :base(message)
    {
    }

    public DuplicatePropertyException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
