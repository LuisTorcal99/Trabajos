using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ExamenFinal.DTO;

namespace ExamenFinal.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static ItemModel CreateModelFromDTO(ObjetoDTO objetoDTO)
        {
            return new ItemModel
            {
                Id = objetoDTO.Id,
                Name = objetoDTO.Nombre,
            };
        }
    }
}

//ImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources",
//Constantes.PLANETAS_POSIBLES.Find(x => (planeta.ImageName + Constantes.IMAGES_EXTENSION) == x) ?? Constantes.PATH_IMAGE_NOT_FOUND),
