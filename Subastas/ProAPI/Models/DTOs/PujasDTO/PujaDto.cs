using RestAPI.Models.DTOs.SubastasDTO;

namespace RestAPI.Models.DTOs.PujasDTO
{
    public class PujaDto : CrearPujaDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
