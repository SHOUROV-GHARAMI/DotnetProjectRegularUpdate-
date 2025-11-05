using InventorySystem.Data;
using InventorySystem.Models;
using InventorySystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly CustomIdService _idService;
        public ItemController(ApplicationDbContext db, CustomIdService idService) { _db = db; _idService = idService; }

        [AllowAnonymous]
        public async Task<IActionResult> Index(Guid inventoryId)
        {
            var items = await _db.Items.Where(i => i.InventoryId == inventoryId).ToListAsync();
            ViewBag.InventoryId = inventoryId;
            return View(items);
        }

        public IActionResult Create(Guid inventoryId) { ViewBag.InventoryId = inventoryId; return View(); }

        [HttpPost]
        public async Task<IActionResult> Create(Guid inventoryId, Item model)
        {
            if (!ModelState.IsValid) return View(model);
            model.InventoryId = inventoryId;
            model.CustomId = await _idService.GenerateAsync(inventoryId);
            _db.Items.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { inventoryId = inventoryId });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var item = await _db.Items.Include(i => i.Inventory).FirstOrDefaultAsync(i => i.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Item model)
        {
            if (!ModelState.IsValid)
            {
                return View("Details", model);
            }

            var item = await _db.Items.FindAsync(model.Id);
            if (item == null) return NotFound();

            // optimistic concurrency: set RowVersion for EF original value
            if (model.RowVersion != null)
            {
                _db.Entry(item).Property("RowVersion").OriginalValue = model.RowVersion;
            }

            item.CustomFieldsJson = model.CustomFieldsJson;
            try
            {
                await _db.SaveChangesAsync();
                TempData["Success"] = "Saved";
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "The item was updated by someone else. Reload and try again.");
                return View("Details", model);
            }

            return RedirectToAction("Details", new { id = item.Id });
        }
    }
}
