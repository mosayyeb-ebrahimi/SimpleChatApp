namespace ChatApp.Client.Services;

public interface INavigation
{
    public object? CurrentViewModel { get; }
    void Navigate<TViewModel>();
    void Navigate(object viewModel);
    void NavigateBack();
}