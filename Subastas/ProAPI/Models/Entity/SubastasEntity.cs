using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.Entity
{
    public class SubastasEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required]
        [MaxLength(10)]
        public int Price { get; set; }
        public ICollection<PujaEntity> Pujas { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
