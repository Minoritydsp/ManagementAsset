using ManagementAsset.Data;
using ManagementAsset.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagementAsset.Controllers
{
    [Authorize]
    public class PeminjamanController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public PeminjamanController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        //GEt: /Peminjaman - User lihat pengajuan miliknya, Admin lihat semua
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var query = _db.Peminjamans
                .Include(p => p.Asset)
                .Include(p => p.User)
                .AsQueryable();

            if (!User.IsInRole("Admin"))
                query = query.Where(p => p.UserId == userId);

            var list = await query.OrderByDescending(p => p.TanggalPinjam).ToListAsync();
            return View(list);
        }

        //GET: /Peminjaman/Ajukan/5
        public async Task<IActionResult> Ajukan(int id)
        {
            var asset = await _db.Assets.FindAsync(id);
            if (asset == null) return NotFound();

            if (asset.Status != AssetStatus.Tersedia)
            {
                TempData["Error"] = "Aset ini tidak tersedia untuk dipinjam.";
                return RedirectToAction("Details", "Asset");
            }

            var model = new Peminjaman { AssetId = id, Asset = asset };
            return View(model);
        }

        //POST: /Peminjaman/Ajukan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ajukan(Peminjaman peminjaman)
        {
            if (string.IsNullOrEmpty(peminjaman.Keperluan))
            {
                ModelState.AddModelError("Keperluan", "Keperluan tidak boleh kosong. ");
                peminjaman.Asset = await _db.Assets.FindAsync(peminjaman.AssetId);
                return View(peminjaman);
            }

            var data = new Peminjaman
            {
                AssetId = peminjaman.AssetId,
                UserId = _userManager.GetUserId(User),
                Keperluan = peminjaman.Keperluan,
                Status = StatusPeminjaman.Menunggu,
                TanggalPinjam = DateTime.Now
            };

            _db.Peminjamans.Add(data);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Pengajuan peminjaman berhasil diajukan.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Peminjaman/Kembalikan/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Kembalikan(int id)
        {
            var userId = _userManager.GetUserId(User);
            var peminjaman = await _db.Peminjamans
                .Include(p => p.Asset)
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (peminjaman == null) return NotFound();

            peminjaman.Status = StatusPeminjaman.Dikembalikan;
            peminjaman.TanggalKembali = DateTime.Now;

            if (peminjaman.Asset != null)
                peminjaman.Asset.Status = AssetStatus.Tersedia;

            await _db.SaveChangesAsync();

            _db.RiwayatAsets.Add(new RiwayatAset
            {
                AssetId = peminjaman.AssetId,
                UserId = userId,
                Aksi = "Aset dikembalikan",
                StatusLama = AssetStatus.Digunakan,
                StatusBaru = AssetStatus.Tersedia,
                TanggalAksi = DateTime.Now
            });
            await _db.SaveChangesAsync();

            TempData["Success"] = "Aset berhasil dikembalikan.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Peminjaman/Approve/5 (Admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id, string? CatatanAdmin)
        {
            var peminjaman = await _db.Peminjamans
                .Include(p => p.Asset)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (peminjaman == null) return NotFound();

            peminjaman.Status = StatusPeminjaman.Disetujui;
            peminjaman.TanggalDisetujui = DateTime.Now;
            peminjaman.ApprovedByUserId = _userManager.GetUserId(User);
            peminjaman.CatatanAdmin = CatatanAdmin;

            if (peminjaman.Asset != null)
                peminjaman.Asset.Status = AssetStatus.Digunakan;

            await _db.SaveChangesAsync();

            TempData["Success"] = "Peminjaman disetujui.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Peminjaman/Reject/5 (Admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reject(int id, string? CatatanAdmin)
        {
            var peminjaman = await _db.Peminjamans
                .FirstOrDefaultAsync(p => p.Id == id);

            if (peminjaman == null) return NotFound();

            peminjaman.Status = StatusPeminjaman.Ditolak;
            peminjaman.CatatanAdmin = CatatanAdmin;

            await _db.SaveChangesAsync();
            TempData["Success"] = "Peminjaman ditolak.";
            return RedirectToAction(nameof(Index));
        }
    }
}