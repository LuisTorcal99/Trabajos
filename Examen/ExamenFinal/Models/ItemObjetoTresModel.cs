using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenFinal.DTO;
using ExamenFinal.View;

namespace ExamenFinal.Models
{
    public class ItemObjetoTresModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public List<int> Productos { get; set; }
        public string IdsObjetoAsString => string.Join(", ", Productos ?? new List<int>());
        public static ItemObjetoTresModel CreateModelFromDTO(ObjetoTresDTO objetoDTO)
        {
            return new ItemObjetoTresModel
            {
                Id = objetoDTO.Id,
                Usuario = objetoDTO.Usuario,
                Productos = objetoDTO.Productos,
                Fecha = objetoDTO.Fecha,
            };
        }
    }
}
