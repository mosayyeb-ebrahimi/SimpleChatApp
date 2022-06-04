using ChatApp.Client.Services;
using System.Windows.Input;

namespace ChatApp.Client.ViewModels;

public class MessageDialogViewModel : DialogData
{
    public ICommand NoCommand { get; }
    private DialogType _type = DialogType.OK;
    public DialogType Type
    {
        get => _type;
        set => SetProperty(ref _type, value);
    }

    private string? _title;
    public string? Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    private string? _message;
    public string? Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }

    public MessageDialogViewModel()
    {
        NoCommand = new RelayCommand(() =>
        {
            DialogResult = false;
            CloseCommand?.Execute(null);
        });
    }
}
