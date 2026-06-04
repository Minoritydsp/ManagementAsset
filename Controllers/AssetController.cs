using ManagementAsset.Data;
using ManagementAsset.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ManagementAsset.Controllers
{
    [Authorize]
    public class AssetController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AssetController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET: Asset
        public async Task<IActionResult> Index()
        {
            var assets = await _db.Assets
                .Include(a => a.Category)
                .Include(a => a.Location)
                .Include(a => a.AssignedToUser)
                .ToListAsync();
            return View(assets);
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
            return View(asset);
        }

        //GET: /Asset/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
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