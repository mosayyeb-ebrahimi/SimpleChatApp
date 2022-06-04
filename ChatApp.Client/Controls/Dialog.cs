using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChatApp.Client.Controls;

public class Dialog : ContentControl
{
    public bool IsDialogOpen
    {
        get => (bool)GetValue(IsDialogOpenProperty);
        set => SetValue(IsDialogOpenProperty, value);
    }

    public static readonly DependencyProperty IsDialogOpenProperty =
        DependencyProperty.Register(nameof(IsDialogOpen), typeof(bool), typeof(Dialog));

    public PathGeometry Icon
    {
        get => (PathGeometry)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(PathGeometry), typeof(Dialog));

    public SolidColorBrush IconColor
    {
        get => (SolidColorBrush)GetValue(IconColorProperty);
        set => SetValue(IconColorProperty, value);
    }

    public static readonly DependencyProperty IconColorProperty =
        DependencyProperty.Register(nameof(IconColor), typeof(SolidColorBrush), typeof(Dialog));

    static Dialog()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Dialog), new FrameworkPropertyMetadata(typeof(Dialog)));
    }
}
