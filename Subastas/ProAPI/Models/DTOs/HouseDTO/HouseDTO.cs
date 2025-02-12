namespace RestAPI.Models.DTOs.HouseDTO
{
    public class HouseDTO : CreateHouseDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
