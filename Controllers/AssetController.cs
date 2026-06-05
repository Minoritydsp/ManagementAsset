using ManagementAsset.Data;
using ManagementAsset.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ManagementAsset.Controllers
{
    [Authorize]
    public class AssetController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public AssetController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        //GET: Asset
        public async Task<IActionResult> Index(string? search, string? status, int? categoryId)
        {
            var query = _db.Assets
                .Include(a => a.Category)
                .Include(a => a.Location)
                .Include(a => a.AssignedToUser)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(a => a.Name.Contains(search) || a.AssetCode.Contains(search));

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<AssetStatus>(status, out var statusEnum))
                query = query.Where(a => a.Status == statusEnum);

            if (categoryId.HasValue)
                query = query.Where(a => a.CategoryId == categoryId);

            ViewBag.search = search;
            ViewBag.status = status;
            ViewBag.categoryId = categoryId;
            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name");

            return View(await query.ToListAsync());
        }

        //GET: /Asset/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var asset = await _db.Assets
                .Include(a => a.Category)
                .Include(a => a.Location)
                .Include(a => a.AssignedToUser)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asset == null) return NotFound();

            var peminjamanAktif = await _db.Peminjamans
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.AssetId == id && p.Status == StatusPeminjaman.Disetujui);

            ViewBag.PeminjamanAktif = peminjamanAktif;

            return View(asset);
        }


        //GET: /Asset/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        //GET: /Asset/Riwayat/5
        public async Task<IActionResult> Riwayat(int id)
        {
            var asset = await _db.Assets.FindAsync(id);
            if (asset == null) return NotFound();

            var riwayat = await _db.RiwayatAsets
                .Include(r => r.User)
                .Where(r => r.AssetId == id)
                .OrderByDescending(r => r.TanggalAksi)
                .ToListAsync();

            ViewBag.AssetName = asset.Name;
            ViewBag.AssetCode = asset.AssetCode;
            ViewBag.AssetId = asset.Id;

            return View(riwayat);
        }

        //POST: /Asset/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Asset asset)
        {
            if (ModelState.IsValid)
            {
                asset.CreatedAt = DateTime.Now;
                _db.Assets.Add(asset);
                await _db.SaveChangesAsync();

                _db.RiwayatAsets.Add(new RiwayatAset
                {
                    AssetId = asset.Id,
                    UserId = _userManager.GetUserId(User),
                    Aksi = "Aset ditambahkan",
                    StatusBaru = asset.Status,
                    TanggalAksi = DateTime.Now
                });
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            LoadDropdowns();
            return View(asset);
        }

        //GET: /Asset/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var asset = await _db.Assets.FindAsync(id);
            if (asset == null) return NotFound();
            LoadDropdowns();
            return View(asset);
        }

        //POST: /Asset/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Asset asset)
        {
            if (id != asset.Id) return NotFound();

            if (ModelState.IsValid)
            {
                asset.UpdatedAt = DateTime.Now;
                _db.Assets.Update(asset);
                await _db.SaveChangesAsync();
                _db.RiwayatAsets.Add(new RiwayatAset
                {
                    AssetId = asset.Id,
                    UserId = _userManager.GetUserId(User),
                    Aksi = "Data aset diperbarui",
                    StatusBaru = asset.Status,
                    TanggalAksi = DateTime.Now
                });
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            LoadDropdowns();
            return View(asset);
        }

        //GET: /Asset/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var asset = await _db.Assets
                .Include(a => a.Category)
                .Include(a => a.Location)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asset == null) return NotFound();
            return View(asset);
        }

        //POST: /Asset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset = await _db.Assets.FindAsync(id);
            if (asset != null)
            {
                _db.Assets.Remove(asset);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private void LoadDropdowns()
        {
            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.Locations = new SelectList(_db.Locations, "Id", "Name");
        }
    }
}