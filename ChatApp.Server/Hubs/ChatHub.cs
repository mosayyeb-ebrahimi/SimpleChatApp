using ChatApp.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Server.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(Message message)
    {
        await Clients.OthersInGroup(message.Group).SendAsync("OnMessage", message);
    }

    public async Task JoinRoom(RoomInfo info)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, info.RoomName);
        await Clients.OthersInGroup(info.RoomName).SendAsync("OnJoined", info.UserName);
    }

    public async Task ExitRoom(RoomInfo info)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, info.RoomName);
        await Clients.OthersInGroup(info.RoomName).SendAsync("OnExited", info.UserName);
    }
}