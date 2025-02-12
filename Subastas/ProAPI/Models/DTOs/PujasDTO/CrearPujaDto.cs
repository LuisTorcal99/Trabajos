using System.ComponentModel.DataAnnotations;
using RestAPI.Models.DTOs.SubastasDTO;
using RestAPI.Models.Entity;

namespace RestAPI.Models.DTOs.PujasDTO
{
    public class CrearPujaDto
    {
        public int IdSubasta { get; set; }
        public int Precio { get; set; }
    }
}