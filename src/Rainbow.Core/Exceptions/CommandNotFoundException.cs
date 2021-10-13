using System;

namespace Rainbow.Core.Exceptions
{
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException() : base() { }
        public CommandNotFoundException(string message) : base(message) { }
        public CommandNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
