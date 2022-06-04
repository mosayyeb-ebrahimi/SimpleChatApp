using ChatApp.Shared.Models;

namespace ChatApp.Client.ViewModels;

public class MessageViewModel : BaseViewModel
{
    private string? _text;
    public string? Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }

    private string? _from;
    public string? From
    {
        get => _from;
        set => SetProperty(ref _from, value);
    }

    private bool _sentByUser;
    public bool SentByUser
    {
        get => _sentByUser;
        set => SetProperty(ref _sentByUser, value);
    }
    public MessageType Type { get; set; } = MessageType.Message;
}
