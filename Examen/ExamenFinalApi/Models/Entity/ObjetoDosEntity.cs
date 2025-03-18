using System.ComponentModel.DataAnnotations;

namespace ExamenFinalApi.Models.Entity
{
    public class ObjetoDosEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
    }
}
