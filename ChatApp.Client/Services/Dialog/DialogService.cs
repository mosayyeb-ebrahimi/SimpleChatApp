using ChatApp.Client.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace ChatApp.Client.Services;

public class DialogService : IDialogService
{
    private static Dictionary<Type, Type> _types = new();

    private readonly IServiceProvider _serviceProvider;
    private readonly Dialog _dialogContent;

    public DialogService(IServiceProvider serviceProvider, Dialog dialogContent)
    {
        _serviceProvider = serviceProvider;
        _dialogContent = dialogContent;
    }

    public Task<TResult> ShowDialog<TResult>() where TResult : DialogData
    {
        var view = Activator.CreateInstance(_types[typeof(TResult)]) as FrameworkElement;
        var dialogData = _serviceProvider.GetRequiredService<TResult>();
        TaskCompletionSource<TResult> taskCompletionSource = new();
        dialogData.Closed += async (result) =>
        {
            taskCompletionSource.SetResult((TResult)result);
            _dialogContent.IsDialogOpen = false;
            await Task.Delay(500);
            _dialogContent.DataContext = null;
            _dialogContent.Content = null;
        };
        _dialogContent.Content = view;
        _dialogContent.DataContext = dialogData;
        _dialogContent.IsDialogOpen = true;
        return taskCompletionSource.Task;
    }

    public Task ShowDialog<TResult>(TResult dialogData) where TResult : DialogData
    {
        var view = Activator.CreateInstance(_types[typeof(TResult)]) as FrameworkElement;
        TaskCompletionSource<TResult> taskCompletionSource = new();
        dialogData.Closed += async (result) =>
        {
            taskCompletionSource.SetResult((TResult)result);
            _dialogContent.IsDialogOpen = false;
            await Task.Delay(500);
            _dialogContent.DataContext = null;
            _dialogContent.Content = null;
        };
        _dialogContent.Content = view;
        _dialogContent.DataContext = dialogData;
        _dialogContent.IsDialogOpen = true;
        return taskCompletionSource.Task;
    }
    public static void Register<TViewModel, TView>() where TViewModel : INotifyPropertyChanged
    {
        _types.Add(typeof(TViewModel), typeof(TView));
    }
}
