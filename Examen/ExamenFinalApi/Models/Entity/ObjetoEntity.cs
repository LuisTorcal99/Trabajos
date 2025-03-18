using System.ComponentModel.DataAnnotations;
using ExamenFinalApi.Models.DTOs.ObjetoDosDto;
using ExamenFinalApi.Models.DTOs.ObjetoTresDto;

namespace ExamenFinalApi.Models.Entity
{
    public class ObjetoEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
    }
}
