using System;
using System.Windows.Input;

namespace ChatApp.Client.ViewModels;

public class RelayCommand : ICommand
{
    private readonly Func<bool>? _condition;
    private readonly Action _action;
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public RelayCommand(Action action, Func<bool>? condition = default)
    {
        _action = action;
        _condition = condition;
    }
    public bool CanExecute(object? parameter) => _condition?.Invoke() ?? true;
    public void Execute(object? parameter) => _action?.Invoke();
}

public class RelayCommand<T> : ICommand
{
    private readonly Predicate<T>? _predicate;
    private readonly Action<T> _action;
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public RelayCommand(Action<T> action, Predicate<T>? predicate = default)
    {
        _action = action;
        _predicate = predicate;
    }
    public bool CanExecute(object? parameter) => _predicate?.Invoke((T)parameter!) ?? true;
    public void Execute(object? parameter) => _action?.Invoke((T)parameter!);
}