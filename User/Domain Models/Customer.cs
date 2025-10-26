using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Domain_Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
