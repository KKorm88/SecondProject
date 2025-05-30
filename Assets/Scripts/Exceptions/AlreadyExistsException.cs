﻿using System;

namespace SecondProject.Exceptions
{
    internal class AlreadyExistsException : Exception
    {
        private const string BaseMessage = "This element already exists in collection!";

        public AlreadyExistsException() : base(BaseMessage) { }

        public AlreadyExistsException(string message) : base(message) { }

        public AlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
