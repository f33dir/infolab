using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using InfoLab1.ViewModels;

namespace InfoLab1.Views;

public partial class LoginWindowView : ReactiveWindow<LoginWindowViewM>
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