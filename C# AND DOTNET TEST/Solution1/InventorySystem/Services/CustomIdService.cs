using InventorySystem.Data;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Services
{
    public class CustomIdService
    {
        private readonly ApplicationDbContext _db;
        public CustomIdService(ApplicationDbContext db) { _db = db; }
        // Generate an ID based on pattern. Supported tokens: {YYYY}, {MM}, {DD}, {SEQ}, {RANDOM:N}
        public async Task<string> GenerateAsync(Guid inventoryId, string pattern = "INV-{YYYY}-{SEQ}")
        {
            // Simple implementation: load inventory, increment sequence, compose id, ensure unique
            var inv = await _db.Inventories.FirstOrDefaultAsync(i => i.Id == inventoryId);
            if (inv == null) throw new InvalidOperationException("Inventory not found");

            // increment sequence in a safe transaction
            using var tx = await _db.Database.BeginTransactionAsync();
            inv.NextSequence = inv.NextSequence + 1;
            await _db.SaveChangesAsync();

            var seq = inv.NextSequence - 1; // previous value used for this id
            var now = DateTime.UtcNow;
            var id = pattern.Replace("{YYYY}", now.Year.ToString())
                            .Replace("{MM}", now.Month.ToString("D2"))
                            .Replace("{DD}", now.Day.ToString("D2"))
                            .Replace("{SEQ}", seq.ToString());

            // RANDOM token: {RANDOM:N} where N digits
            var randTokenStart = id.IndexOf("{RANDOM:", StringComparison.OrdinalIgnoreCase);
            if (randTokenStart >= 0)
            {
                var end = id.IndexOf('}', randTokenStart);
                if (end > randTokenStart)
                {
                    var inside = id.Substring(randTokenStart + 8, end - (randTokenStart + 8));
                    if (int.TryParse(inside, out var n) && n > 0)
                    {
                        var rnd = new Random();
                        var num = rnd.Next((int)Math.Pow(10, n - 1), (int)Math.Pow(10, n));
                        id = id.Substring(0, randTokenStart) + num.ToString() + id.Substring(end + 1);
                    }
                }
            }

            // ensure uniqueness -- retry a few times if collision
            var attempt = 0;
            while (await _db.Items.AnyAsync(i => i.InventoryId == inventoryId && i.CustomId == id))
            {
                if (++attempt > 5) throw new InvalidOperationException("Unable to generate unique id");
                id = id + "-" + Guid.NewGuid().ToString("n").Substring(0, 6);
            }

            await tx.CommitAsync();
            return id;
        }

        // Generate a preview id without incrementing sequence or committing a transaction.
        public async Task<string> GeneratePreviewAsync(Guid inventoryId, string pattern = "INV-{YYYY}-{SEQ}")
        {
            var inv = await _db.Inventories.FirstOrDefaultAsync(i => i.Id == inventoryId);
            if (inv == null) throw new InvalidOperationException("Inventory not found");
            var seq = inv.NextSequence; // do not change
            var now = DateTime.UtcNow;
            var id = pattern.Replace("{YYYY}", now.Year.ToString())
                            .Replace("{MM}", now.Month.ToString("D2"))
                            .Replace("{DD}", now.Day.ToString("D2"))
                            .Replace("{SEQ}", seq.ToString());

            var randTokenStart = id.IndexOf("{RANDOM:", StringComparison.OrdinalIgnoreCase);
            if (randTokenStart >= 0)
            {
                var end = id.IndexOf('}', randTokenStart);
                if (end > randTokenStart)
                {
                    var inside = id.Substring(randTokenStart + 8, end - (randTokenStart + 8));
                    if (int.TryParse(inside, out var n) && n > 0)
                    {
                        var rnd = new Random();
                        var num = rnd.Next((int)Math.Pow(10, n - 1), (int)Math.Pow(10, n));
                        id = id.Substring(0, randTokenStart) + num.ToString() + id.Substring(end + 1);
                    }
                }
            }
            return id;
        }
    }
}
