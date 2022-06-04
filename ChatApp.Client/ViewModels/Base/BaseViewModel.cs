using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChatApp.Client.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public bool SetProperty<T>(ref T reference, T value, [CallerMemberName] in string? propertyName = null)
    {
        if (Equals(reference, value))
            return false;

        reference = value;
        OnPropertyChanged(propertyName);
        return true;
    }
    protected virtual void OnPropertyChanged(string? propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}