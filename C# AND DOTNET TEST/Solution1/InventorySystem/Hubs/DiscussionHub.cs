using InventorySystem.Models;
using Microsoft.AspNetCore.SignalR;

namespace InventorySystem.Hubs
{
    public class DiscussionHub : Hub
    {
        // Send comment to group (item-specific)
        public async Task SendComment(string itemId, string userId, string message)
        {
            var payload = new { ItemId = itemId, UserId = userId, Message = message, CreatedAt = DateTime.UtcNow };
            await Clients.Group(itemId).SendAsync("ReceiveComment", payload);
        }

        public Task JoinItemGroup(string itemId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, itemId);
        }

        public Task LeaveItemGroup(string itemId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, itemId);
        }
    }
}
