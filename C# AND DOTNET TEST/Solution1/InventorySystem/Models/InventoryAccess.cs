using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Models
{
    public class InventoryAccess
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid InventoryId { get; set; }
        [ForeignKey("InventoryId")]
        public Inventory? Inventory { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
        // Role or permission string (Admin/Creator/User or granular JSON)
        public string Role { get; set; } = "User";
    }
}
