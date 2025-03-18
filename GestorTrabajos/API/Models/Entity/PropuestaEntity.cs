using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Entity
{
    public class PropuestaEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public ICollection<User> Users { get; } = [];
    }
}
