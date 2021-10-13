using System;

namespace Rainbow.Core.Exceptions
{
    public class PermissionsNotFoundException : Exception
    {
        public PermissionsNotFoundException() : base() { }
        public PermissionsNotFoundException(string message) : base(message) { }
        public PermissionsNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
