using System.Windows;
using System.Windows.Controls;

namespace ChatApp.Client.Views;

/// <summary>
/// Interaction logic for ChatView.xaml
/// </summary>
public partial class ChatView : UserControl
{
    public ChatView() => InitializeComponent();

    private void SendBtn_Click(object sender, RoutedEventArgs e)
    {
        scroll.ScrollToBottom();
    }
}
