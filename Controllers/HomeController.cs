using ManagementAsset.Data;
using ManagementAsset.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagementAsset.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var assets = await _db.Assets
                .Include(a => a.Category)
                .Include(a => a.Location)
                .ToListAsync();

            var vm = new DashboardViewModel
            {
                TotalAset = assets.Count,
                TotalTersedia = assets.Count(a => a.Status == AssetStatus.Tersedia),
                TotalDigunakan = assets.Count(a => a.Status == AssetStatus.Digunakan),
                TotalPerbaikan = assets.Count(a => a.Status == AssetStatus.Perbaikan),
                TotalRusak = assets.Count(a => a.Status == AssetStatus.Rusak),

                AsetPerKategori = assets
                    .GroupBy(a => a.Category!.Name)
                    .ToDictionary(g => g.Key, g => g.Count()),

                AsetPerLokasi = assets
                    .GroupBy(a => a.Location!.Name)
                    .ToDictionary(g => g.Key, g => g.Count()),

                AsetTerbaru = assets
                    .OrderByDescending(a => a.CreatedAt)
                    .Take(5)
                    .ToList()
            };

            return View(vm);
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
