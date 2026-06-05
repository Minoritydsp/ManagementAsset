using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementAsset.Models
{
    public enum StatusPeminjaman
    {
        Menunggu,
        Disetujui,
        Ditolak,
        Dikembalikan
    }

    public class Peminjaman
    {
        public int Id { get; set; }

        [Required]
        public int AssetId { get; set; }
        public Asset? Asset { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }

        public string? ApprovedByUserId { get; set; }
        public ApplicationUser? ApprovedByUser { get; set; }

        [Required]
        [MaxLength(500)]
        public string Keperluan { get; set; } = string.Empty;

        public StatusPeminjaman Status { get; set; } = StatusPeminjaman.Menunggu;

        public DateTime TanggalPinjam { get; set; } = DateTime.Now;
        public DateTime? TanggalDisetujui { get; set; }
        public DateTime? TanggalKembali { get; set; }

        [MaxLength(500)]
        public string? CatatanAdmin { get; set; }
    }
}