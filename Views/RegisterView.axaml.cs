using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using InfoLab1.ViewModels;

namespace InfoLab1.Views;

public partial class RegisterView : ReactiveUserControl<RegisterViewM>
{
    private RegisterViewM VM;
    public RegisterView()
    {
        VM = new RegisterViewM();
        DataContext = VM;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Register_OnClick(object? sender, RoutedEventArgs e)
    {
        VM.Register();
    }

    private void Auth_OnClick(object? sender, RoutedEventArgs e)
    {
        var register = new LoginWindowView();
        register.Show();
        ((Window)this.Parent).Close();
    }
}