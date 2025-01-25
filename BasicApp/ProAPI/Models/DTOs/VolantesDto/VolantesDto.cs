namespace RestAPI.Models.DTOs.VolantesDto
{
    public class VolantesDto : CreateVolantesDto
    {
        public int Id { get; set; }
        public DateTime FechaSalida { get; set; }
    }
}
