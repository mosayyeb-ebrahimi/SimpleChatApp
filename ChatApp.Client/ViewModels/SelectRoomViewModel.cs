using ChatApp.Client.Services;
using ChatApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ChatApp.Client.ViewModels;

public class SelectRoomViewModel : BaseViewModel
{
    public List<string> Rooms { get; } = new();
    public ICommand EnterCommand { get; }

    public SelectRoomViewModel(INavigation navigation, IChatService chatService, IDialogService dialogService)
    {
        Rooms.Add("Room A");
        Rooms.Add("Room B");
        Rooms.Add("Room C");
        EnterCommand = new RelayCommand(async () =>
        {
            IsConnecting = true;
            try
            {
                var info = new RoomInfo(SelectedRoom!, Name!.Trim());
                if (!chatService.IsConnected)
                    await chatService.Connect();

                await chatService.JoinRoom(info);
                navigation.Navigate(new ChatViewModel(info, navigation, chatService, dialogService));
            }
            catch (Exception)
            {
                IsConnecting = false;
                await dialogService.ShowDialog(new MessageDialogViewModel
                {
                    Title = "Connection Error",
                    Message = "please check your connection and try again",
                    Type = DialogType.OK,
                    MessageType = DialogMessageType.Error
                });
            }
            IsConnecting = false;
        }, () => !string.IsNullOrWhiteSpace(SelectedRoom) && !string.IsNullOrWhiteSpace(Name) && !IsConnecting);
    }

    private string? _selectedRoom;
    public string? SelectedRoom
    {
        get => _selectedRoom;
        set => SetProperty(ref _selectedRoom, value);
    }

    private string? _name;
    public string? Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private bool _isConnecting;
    public bool IsConnecting
    {
        get => _isConnecting;
        set => SetProperty(ref _isConnecting, value);
    }
}

