using Microsoft.AspNetCore.Identity;

namespace ManagementAsset.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
    }
}