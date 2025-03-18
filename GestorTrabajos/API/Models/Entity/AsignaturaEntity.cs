using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Entity
{
    public class AsignaturaEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CursoId")]
        public int CursoId { get; set; }
        public string Nombre { get; set; }
        public ICollection<User> Users { get; } = [];
        public CursoEntity Curso { get; set; }
    }
}
