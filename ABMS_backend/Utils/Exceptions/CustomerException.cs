using ABMS_backend.Utils.Validates;
using System;

namespace ABMS_backend.Utils.Exceptions
{
    public class CustomException : Exception
    {
        private ErrorApp errorApp;
        private int? codeError;

        public CustomException(ErrorApp errorApp) : base(errorApp.description)
        {
            this.errorApp = errorApp;
        }

        public CustomException(int code, string mess) : base(mess)
        {
            codeError = code;
        }

        public CustomException(string mess) : base(mess)
        {
        }

        public ErrorApp GetErrorApp()
        {
            return errorApp;
        }

        public int? GetCodeError()
        {
            return codeError;
        }
    }
}
