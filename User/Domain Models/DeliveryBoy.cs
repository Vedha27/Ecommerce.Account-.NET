using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Domain_Models
{
    public class DeliveryBoy
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [MaxLength(20)]
        public string VehicleNumber { get; set; }

        [MaxLength(100)]
        public string CurrentLocation { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
