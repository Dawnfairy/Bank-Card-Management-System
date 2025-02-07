using BankCardProject.Helpers;
using System.Net;

namespace BankCardProject.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message, int errorCode = 2001)
            : base(message, errorCode, HttpStatusCode.NotFound) { }
    }

    public class BadRequestException : CustomException
    {
        public BadRequestException(string message, int errorCode = 1000)
            : base(message, errorCode, HttpStatusCode.BadRequest) { }
    }

    public class DuplicateRecordException : CustomException
    {
        public DuplicateRecordException(string message, int errorCode = 1002)
            : base(message, errorCode, HttpStatusCode.Conflict) { }
    }

    public class InvalidParameterException : CustomException
    {
        public InvalidParameterException(string message, int errorCode = 1015)
            : base(message, errorCode, HttpStatusCode.BadRequest) { }
    }

    public class OperationFailedException : CustomException
    {
        public OperationFailedException(string message, int errorCode = 1004)
            : base(message, errorCode, HttpStatusCode.InternalServerError) { }
    }

    public class DataAccessException : CustomException
    {
        public DataAccessException(string message, int errorCode = 1005)
            : base(message, errorCode, HttpStatusCode.ServiceUnavailable) { }
    }

    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException(string message, int errorCode = 1009)
            : base(message, errorCode, HttpStatusCode.Unauthorized) { }
    }
}
