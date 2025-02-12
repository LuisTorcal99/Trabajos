using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.DTOs.HouseDTO
{
    public class CreateHouseDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Max char is 50")]
        public string Name { get; set; }
        [Required(ErrorMessage = "city is required")]
        [MaxLength(50, ErrorMessage = "Max char is 50")]
        public string City { get; set; }
        [Required(ErrorMessage = "state is required")]
        [MaxLength(50, ErrorMessage = "Max char is 50")]
        public string State { get; set; }
        [Required(ErrorMessage = "photo is required")]
        public string Photo { get; set; }
        [Required(ErrorMessage = "availableUnits is required")]
        public int AvailableUnits { get; set; }
        public bool Wifi { get; set; }
        public bool Laundry { get; set; }
    }
}
