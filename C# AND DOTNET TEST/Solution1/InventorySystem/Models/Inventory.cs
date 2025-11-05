using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Models
{
    public class Inventory
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string OwnerId { get; set; } = string.Empty;
        [ForeignKey("OwnerId")]
        public ApplicationUser? Owner { get; set; }
        public int NextSequence { get; set; } = 1; // for per-inventory sequences
    // Custom ID pattern stored as a format string, e.g. "INV-{YYYY}-{SEQ}" or with RANDOM token {RANDOM:5}
    public string? CustomIdPattern { get; set; }
        public ICollection<Item>? Items { get; set; }
        public ICollection<InventoryAccess>? Accesses { get; set; }
    }
}
