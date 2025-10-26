using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Domain_Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public Guid UserGuid { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Phone]
        [MaxLength(20)]
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public int IsActive { get; set; } = 0;

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
