namespace ABMS_backend.DTO.ServiceTypeDTO
{
    public class ServiceTypeInsert
    {
        public string buildingId {  get; set; }
        public string name {  get; set; }

        public string Validate()
        {
            if (string.IsNullOrEmpty(name))
            {
                return "Name is required!";
            }

            return null;
        }
    }
}
