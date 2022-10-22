using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace InfoLab1.Views;

public partial class RegisterWindowView : Window
{
    public RegisterWindowView()
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