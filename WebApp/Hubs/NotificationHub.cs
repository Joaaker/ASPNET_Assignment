﻿using Microsoft.AspNetCore.SignalR;

namespace WebApp.Hubs;

public class NotificationHub : Hub
{
    //Generated by ChatGPT-04-mini-high - Checks if the logged in users are Admin on connection and adds them to SignalR group "Admins" if they are
    public override async Task OnConnectedAsync()
    {
        if (Context.User != null && Context.User.IsInRole("Admin"))
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
        
        await base.OnConnectedAsync();
    }
}