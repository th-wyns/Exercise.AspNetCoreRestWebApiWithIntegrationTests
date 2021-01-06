using System;

namespace Users.Repositories
{
    public class RepositoryException : Exception
    {
        public string Repository { get; set; }
        public string Operation { get; set; }
        public ErrorType ErrorType { get; set; }

        public RepositoryException(string repository, string operation, ErrorType errorType, string message = null, Exception innerException = null)
            : base(message, innerException)
        {
            Repository = repository;
            Operation = operation;
            ErrorType = errorType;
        }
    }
}
