using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace InfoLab1.Views;

public partial class LoginWindowView : Window
{
    public LoginWindowView()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}