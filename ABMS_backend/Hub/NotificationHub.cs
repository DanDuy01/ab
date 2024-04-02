using Microsoft.AspNetCore.SignalR;

public class NotificationHub : Hub
{
    public async Task JoinGroup(string buildingId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, buildingId);
    }

    public async Task LeaveGroup(string buildingId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, buildingId);
    }
}
