using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Domain_Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [MaxLength(100)]
        public string ShopName { get; set; }

        [MaxLength(20)]
        public string GSTNumber { get; set; }

        [MaxLength(50)]
        public string BankAccountNumber { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
