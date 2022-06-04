using ChatApp.Client.ViewModels;
using System;
using System.Windows.Input;

namespace ChatApp.Client.Services;

public abstract class DialogData : BaseViewModel
{
    public event Action<DialogData>? Closed;
    public DialogData()
    {
        CloseCommand = new RelayCommand(() =>
        {
            Closed?.Invoke(this);
        });
    }

    public ICommand? CloseCommand { get; }
    public bool DialogResult { get; set; } = true;
    public DialogMessageType MessageType { get; set; } = DialogMessageType.None;

}
