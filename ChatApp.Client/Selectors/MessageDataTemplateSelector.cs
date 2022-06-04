using ChatApp.Client.ViewModels;
using ChatApp.Shared.Models;
using System.Windows;
using System.Windows.Controls;

namespace ChatApp.Client;

public class MessageDataTemplateSelector : DataTemplateSelector
{
    public override DataTemplate? SelectTemplate(object item, DependencyObject container)
    {
        if (container is FrameworkElement element && item is MessageViewModel vm)
        {
            var resourceName = vm.Type == MessageType.Message ? "MessageDataTemplate" : "JoinMessageDataTemplate";
            return element.FindResource(resourceName) as DataTemplate;
        }

        return null;
    }
}
