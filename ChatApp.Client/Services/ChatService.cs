using ChatApp.Shared.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ChatApp.Client.Services;

public class ChatService : IChatService
{
    private readonly HubConnection _connection;
    public bool IsConnected => _connection.State == HubConnectionState.Connected;

    public event Action<Message>? OnMessage;
    public event Action<string>? OnJoined;
    public event Action<string>? OnExited;

    public ChatService(HubConnection connection)
    {
        _connection = connection;
        _connection.On<Message>(nameof(OnMessage), (message) => OnMessage?.Invoke(message));
        _connection.On<string>(nameof(OnJoined), (name) => OnJoined?.Invoke(name));
        _connection.On<string>(nameof(OnExited), (name) => OnExited?.Invoke(name));
    }

    public async Task Connect() => await _connection.StartAsync();
    public async Task Disconnect() => await _connection.StopAsync();
    public async Task ExitRoom(RoomInfo info) => await _connection.InvokeAsync(nameof(ExitRoom), info);
    public async Task JoinRoom(RoomInfo info) => await _connection.InvokeAsync(nameof(JoinRoom), info);
    public async Task SendMessage(Message message) => await _connection.InvokeAsync(nameof(SendMessage), message);
}
