using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BasicAPP.DTO;

namespace BasicAPP.Model
{
    public class ItemGridModel
    {
        public string Base { get; set; }
        public string Aro { get; set; }
        public string Pedales { get; set; }
        public bool WorkInConsole { get; set; }
        public DateTime FechaSalida { get; set; }

        public static ItemGridModel CreateModelFromDTO(VolantesDTO volantesDTO)
        {
            return new ItemGridModel
            {
                Base = volantesDTO.Base,
                Aro = volantesDTO.Aro,
                Pedales = volantesDTO.Pedales,
                WorkInConsole = volantesDTO.WorkInConsole,
                FechaSalida = volantesDTO.FechaSalida
            };
        }
    }
}
