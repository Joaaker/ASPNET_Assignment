using Microsoft.AspNetCore.SignalR;

namespace WebApp.Hubs;

public class NotficationHub : Hub
{
    public async Task SendNotificationToAll(object notification)
    {
        await Clients.All.SendAsync("AllReceiveNotification", notification);
    }

    public async Task SendNotificationToAdmins(object notification)
    {
        await Clients.All.SendAsync("AdminReceiveNotification", notification);
    }

}
