using ABMS_backend.DTO.RoomServiceDTO;

namespace ABMS_backend.DTO.ServiceChargeDTO
{
    public class ServiceChargeResponseDTO
    {
        public int year { get; set; }

        public int month { get; set; }

        public float total { get; set; }

        public int status { get; set; }

        public List<RoomServiceResponseDTO> detail { get; set; }
    }
}
