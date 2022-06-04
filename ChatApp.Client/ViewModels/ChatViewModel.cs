using ChatApp.Client.Services;
using ChatApp.Shared.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ChatApp.Client.ViewModels;

public class ChatViewModel : BaseViewModel
{
    private readonly IChatService _chatService;
    public ObservableCollection<MessageViewModel> Messages { get; } = new();

    public ChatViewModel(RoomInfo info, INavigation navigation, IChatService chatService, IDialogService dialogService)
    {
        RoomName = info.RoomName;
        _chatService = chatService;

        _chatService.OnJoined += (name) =>
        {
            Messages.Add(new MessageViewModel
            {
                Type = MessageType.JoinExit,
                Text = $"{name} Joined"
            });
        };
        _chatService.OnExited += (name) =>
        {
            Messages.Add(new MessageViewModel
            {
                Type = MessageType.JoinExit,
                Text = $"{name} Exited"
            });
        };
        _chatService.OnMessage += (message) =>
        {
            Messages.Add(new MessageViewModel { Text = message.Text, From = message.From });
        };

        SendCommand = new RelayCommand(async () =>
        {
            var message = new Message(info.UserName, info.RoomName, Text!.Trim());
            await _chatService.SendMessage(message);
            Messages.Add(new MessageViewModel { Text = Text.Trim(), SentByUser = true });
            Text = string.Empty;
        }, () => !string.IsNullOrWhiteSpace(Text));

        ExitCommand = new RelayCommand(async () =>
        {
            var viewModel = new MessageDialogViewModel
            {
                Title = "Exit Room",
                Message = $"Are you sure you want exit {info.RoomName}?",
                MessageType = DialogMessageType.Question,
                Type = DialogType.YesNo
            };
            await dialogService.ShowDialog(viewModel);
            if (viewModel.DialogResult)
            {
                await _chatService.ExitRoom(info);
                navigation.NavigateBack();
            }
        });
    }

    public ICommand SendCommand { get; }
    public ICommand ExitCommand { get; }
    public string RoomName { get; }

    private string? _text;
    public string? Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }
}
