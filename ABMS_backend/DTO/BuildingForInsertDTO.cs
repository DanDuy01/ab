namespace ABMS_backend.DTO
{
    public class BuildingForInsertDTO
    {
        public string? name { get; set; }

        public string? address { get; set; }

        public int? number_of_floor { get; set; }

        public int? room_each_floor { get; set; }

        //public string Validate()
        //{
        //    if (String.IsNullOrEmpty(name))
        //    {
        //        return "Name is required!";
        //    }

        //    if (String.IsNullOrEmpty(address))
        //    {
        //        return "Address is required!";
        //    }
        //    return null;
        //}
    }
}
