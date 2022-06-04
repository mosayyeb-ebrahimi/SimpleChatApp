using ChatApp.Client.Controls;
using ChatApp.Client.Services;
using ChatApp.Client.ViewModels;
using ChatApp.Client.Views.Dialogs;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace ChatApp.Client;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        DialogService.Register<MessageDialogViewModel, MessageDialogView>();
        App.Current.MainWindow = new MainWindow();
        IServiceProvider serviceProvider = ConfigureServices();
        App.Current.MainWindow.DataContext = serviceProvider.GetRequiredService<MainViewModel>();
        App.Current.MainWindow.Show();
    }
    private IServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddSingleton<INavigation, NavigationManager>()
            .AddSingleton<IDialogService, DialogService>(s =>
                 new DialogService(s, Current.MainWindow.FindName("dialog") as Dialog ?? throw new Exception("dialog not found")
                 ))
            .AddTransient<IChatService, ChatService>(_ => new ChatService(
                 new HubConnectionBuilder().WithUrl("http://localhost:5000/chat")
                     .Build()))
            .AddTransient<MainViewModel>()
            .AddTransient<SelectRoomViewModel>()
            .AddTransient<ChatViewModel>()
            .BuildServiceProvider();
    }
}
