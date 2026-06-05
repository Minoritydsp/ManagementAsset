namespace ManagementAsset.Models
{
    public class DashboardViewModel
    {
        public int TotalAset { get; set; }
        public int TotalTersedia { get; set; }
        public int TotalDigunakan { get; set; }
        public int TotalPerbaikan { get; set; }
        public int TotalRusak { get; set; }

        public Dictionary<string, int> AsetPerKategori { get; set; } = new();
        public Dictionary<string, int> AsetPerLokasi { get; set; } = new();
        public List<Asset> AsetTerbaru { get; set; } = new();
    }
}