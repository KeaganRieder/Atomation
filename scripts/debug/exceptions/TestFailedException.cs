namespace Atomation.Exceptions;

using System;

public class TestFailedException : Exception
{
    public TestFailedException(string message) : base(message){}
}
