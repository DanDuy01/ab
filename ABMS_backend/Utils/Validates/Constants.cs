namespace ABMS_backend.Utils.Validates
{
    public class Constants
    {
        public const string PUBLIC_REQUEST_MAPPING_PREFIX = "/public/api";
        public const string REQUEST_MAPPING_PREFIX = "/api";
        public const string VERSION_API_V1 = "/v1";

        public enum STATUS
        {
            IN_ACTIVE = 0,
            ACTIVE = 1,      
            SENT = 2,
            APPROVED = 3,
            REJECTED = 4,
            PAID = 5,
            NOT_PAID = 6
        }

        public enum PARKING_CARD_TYPE
        {
            MOTOR_BIKE = 1,
            CAR = 2,
            ELECTRIC_BICYCLE = 3
        }

        public enum ROLE
        {
            ADMIN = 0,
            CMB = 1,
            RECEPTIONIST = 2,
            ROOM = 3
        }
    }
}
