using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cros.DataAccess.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string CustomerNo { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [StringLength(50)]
        public string? MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }

        [StringLength(20)]
        public string? NameSuffix { get; set; }

        [StringLength(254)]
        public string? EmailAddress { get; set; }

        [StringLength(50)]
        public string? MobilePhoneNo { get; set; }

        public string? Signature { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedAt { get; set; }
    }
}