using ChatApp.Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;

namespace ChatApp.Client.Services;

public class NavigationManager : BaseViewModel, INavigation
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Stack _navigationStack = new();

    public object? CurrentViewModel => _navigationStack.Count > 0 ? _navigationStack.Peek() : null;

    public NavigationManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public void Navigate<TViewModel>()
    {
        var vm = _serviceProvider.GetService<TViewModel>();
        if (vm is not null)
        {
            _navigationStack.Push(vm);
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
    public void Navigate(object viewModel)
    {
        _navigationStack.Push(viewModel);
        OnPropertyChanged(nameof(CurrentViewModel));
    }
    public void NavigateBack()
    {
        _navigationStack.Pop();
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}