using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenFinal.DTO;

namespace ExamenFinal.Models
{
    public class ItemObjetoDosModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }

        public static ItemObjetoDosModel CreateModelFromDTO(ObjetoDosDTO objetoDTO)
        {
            return new ItemObjetoDosModel
            {
                Id = objetoDTO.Id,
                Nombre = objetoDTO.Nombre,
                Precio = objetoDTO.Precio
            };
        }
    }
}
