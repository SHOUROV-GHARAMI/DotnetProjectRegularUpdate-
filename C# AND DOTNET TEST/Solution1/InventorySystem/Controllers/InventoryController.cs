using InventorySystem.Data;
using InventorySystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public InventoryController(ApplicationDbContext db) { _db = db; }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var list = await _db.Inventories.Include(i => i.Owner).ToListAsync();
            return View(list);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Inventory model)
        {
            if (!ModelState.IsValid) return View(model);
            _db.Inventories.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Inventory settings - manage Custom ID pattern
        public async Task<IActionResult> Settings(Guid id)
        {
            var inv = await _db.Inventories.FirstOrDefaultAsync(i => i.Id == id);
            if (inv == null) return NotFound();
            return View(inv);
        }

        [HttpPost]
        public async Task<IActionResult> Settings(Guid id, string customIdPattern)
        {
            var inv = await _db.Inventories.FirstOrDefaultAsync(i => i.Id == id);
            if (inv == null) return NotFound();
            inv.CustomIdPattern = customIdPattern;
            _db.Update(inv);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> PreviewCustomId(Guid id, string pattern, [FromServices] InventorySystem.Services.CustomIdService idService)
        {
            var inv = await _db.Inventories.FirstOrDefaultAsync(i => i.Id == id);
            if (inv == null) return NotFound();
            try
            {
                var sample = await idService.GeneratePreviewAsync(id, pattern ?? inv.CustomIdPattern ?? "INV-{YYYY}-{SEQ}");
                return Json(new { ok = true, sample });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, error = ex.Message });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> PreviewCustomId(Guid id, string pattern)
        {
            // GET-friendly preview endpoint (uses DI service provider)
            var idService = HttpContext.RequestServices.GetService(typeof(InventorySystem.Services.CustomIdService)) as InventorySystem.Services.CustomIdService;
            if (idService == null) return StatusCode(500, new { ok = false, error = "Service unavailable" });

            var inv = await _db.Inventories.FirstOrDefaultAsync(i => i.Id == id);
            if (inv == null) return NotFound();
            try
            {
                var sample = await idService.GeneratePreviewAsync(id, pattern ?? inv.CustomIdPattern ?? "INV-{YYYY}-{SEQ}");
                return Json(new { ok = true, sample });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, error = ex.Message });
            }
        }
    }
}
