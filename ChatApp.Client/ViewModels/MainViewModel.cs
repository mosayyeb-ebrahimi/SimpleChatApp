using ChatApp.Client.Services;

namespace ChatApp.Client.ViewModels;

public class MainViewModel : BaseViewModel
{
    public INavigation Navigation { get; }

    public MainViewModel(INavigation navigation)
    {
        Navigation = navigation;
        navigation.Navigate<SelectRoomViewModel>();
    }
}