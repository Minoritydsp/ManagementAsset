using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementAsset.Models
{
    public enum AssetStatus
    {
        Tersedia,
        Digunakan,
        Perbaikan,
        Rusak,
        Dihapus
    }

    public class Asset
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string AssetCode { get; set; } = string.Empty;
        public string? Description { get; set; }

        [Required]
        public AssetStatus Status { get; set; } = AssetStatus.Tersedia;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        public DateTime PurchaseDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Foreign Keys
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int LocationId { get; set; }
        public Location? Location { get; set; }

        public string? AssignedToUserId { get; set; }
        public ApplicationUser? AssignedToUser { get; set; }
    }
}