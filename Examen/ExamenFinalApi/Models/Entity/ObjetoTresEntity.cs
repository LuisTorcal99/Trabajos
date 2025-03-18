using System.ComponentModel.DataAnnotations;
using ExamenFinalApi.Models.DTOs.ObjetoDto;

namespace ExamenFinalApi.Models.Entity
{
    public class ObjetoTresEntity
    {
        public int Id { get; set; }
        public int Usuario { get; set; }
        public List<int> Productos { get; set; }
        public DateTime Fecha { get; set; }
    }
}
