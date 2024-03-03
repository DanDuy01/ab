namespace ABMS_backend.Utils.Validates
{
    public class ErrorApp
    {
        public int code { get; set; }
        public string description { get; set; }

        ErrorApp(int code, string description)
        {
            this.code = code;
            this.description = description;
        }

        public static readonly ErrorApp SUCCESS = new ErrorApp(200, "Successful");
        public static readonly ErrorApp BAD_REQUEST = new ErrorApp(400, "Bad request");
        public static readonly ErrorApp BAD_REQUEST_PATH = new ErrorApp(400, "Invalid path parameter");
        public static readonly ErrorApp UNAUTHORIZED = new ErrorApp(401, "Incorrect username or password");
        public static readonly ErrorApp FORBIDDEN = new ErrorApp(403, "Access denied");
        public static readonly ErrorApp INTERNAL_SERVER = new ErrorApp(500, "Error in to process. Please try again after!");

        public static readonly ErrorApp OBJECT_NOT_FOUND = new ErrorApp(4001, "Object not found");
        public static readonly ErrorApp ACCOUNT_EXISTED = new ErrorApp(4002, "Account has existed!");


    }
}