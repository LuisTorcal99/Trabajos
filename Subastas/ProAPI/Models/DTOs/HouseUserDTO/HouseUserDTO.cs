namespace RestAPI.Models.DTOs.HouseUserDTO
{
    public class HouseUserDTO : CreateHouseUserDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
