﻿namespace Console_Project.Exceptions;

public class NotFoundException:Exception
{
    public NotFoundException(string message) : base(message) { }
}
