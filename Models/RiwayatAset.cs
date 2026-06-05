using System.ComponentModel.DataAnnotations;

namespace ManagementAsset.Models
{
    public class RiwayatAset
    {
        public int Id { get; set; }

        [Required]
        public int AssetId { get; set; }
        public Asset? Asset { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [Required]
        [MaxLength(200)]
        public string Aksi { get; set; } = string.Empty;

        public AssetStatus? StatusLama { get; set; }
        public AssetStatus? StatusBaru { get; set; }

        [MaxLength(500)]
        public string? Keterangan { get; set; }

        public DateTime TanggalAksi { get; set; } = DateTime.Now;
    }
}