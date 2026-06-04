using System.ComponentModel.DataAnnotations;

namespace ManagementAsset.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
    }
}