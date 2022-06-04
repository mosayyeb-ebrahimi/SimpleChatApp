using ChatApp.Shared.Models;
using System;
using System.Threading.Tasks;

namespace ChatApp.Client.Services;

public interface IChatService
{
    Task Connect();
    Task Disconnect();
    Task SendMessage(Message message);
    Task JoinRoom(RoomInfo info);
    Task ExitRoom(RoomInfo info);
    public bool IsConnected { get; }

    event Action<string>? OnJoined;
    event Action<string>? OnExited;
    event Action<Message>? OnMessage;
}
