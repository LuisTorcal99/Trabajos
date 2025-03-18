using System;
using System.Collections.Generic;
using System.Linq;
using ExamenFinal.DTO;

namespace ExamenFinal.Models
{
    public class OverViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> IdsObjetoDos { get; set; }
        public List<int> IdsObjetoTres { get; set; }

        public string IdsObjetoDosAsString => string.Join(", ", IdsObjetoDos ?? new List<int>());

        public string IdsObjetoTresAsString => string.Join(", ", IdsObjetoTres ?? new List<int>());

        //public static OverViewModel CreateModelFromDTO(ObjetoDTO objetoDTO)
        //{
        //    return new OverViewModel
        //    {
        //        Id = objetoDTO.Id,
        //        Name = objetoDTO.Name,
        //        IdsObjetoDos = objetoDTO.IdsObjetoDos,
        //        IdsObjetoTres = objetoDTO.IdsObjetoTres,
        //    };
        //}
    }
}
