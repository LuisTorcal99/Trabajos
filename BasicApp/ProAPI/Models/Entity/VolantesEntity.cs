using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.Entity
{
    public class VolantesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Base { get; set; }
        [Required]
        [MaxLength(25)]
        public string Aro { get; set; }
        [Required]
        [MaxLength(25)]
        public string Pedales { get; set; }
        [Required]
        public bool WorkInConsole { get; set; }
        public DateTime FechaSalida { get; set; }
    }
}
