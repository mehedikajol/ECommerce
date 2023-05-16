﻿namespace ECommerce.Core.Exceptions;

public class DuplicateNameException : Exception
{
    public DuplicateNameException()
    {
    }

    public DuplicateNameException( string message)
        :base(message)
    {
    }

    public DuplicateNameException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
