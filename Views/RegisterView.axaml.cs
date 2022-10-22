using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using InfoLab1.Services;
using InfoLab1.ViewModels;

namespace InfoLab1.Views;

public partial class RegisterView : ReactiveUserControl<RegisterViewM>
{
    public TextBlock error;
    private RegisterViewM VM;
    public RegisterView()
    {
        
        VM = new RegisterViewM();
        DataContext = VM;
        InitializeComponent();
        error = this.FindControl<TextBlock>("Error");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Register_OnClick(object? sender, RoutedEventArgs e)
    {
        switch (VM.Register()){
            case LoginResult.BadPassword:
                error.Text = "Плохой пароль";
                break;
            case LoginResult.DifferentPasswords:
                error.Text = "Пароли не совпадают";
                break;
            case LoginResult.NoUser:
                error.Text = "Пользователь не найден";
                break;
            case LoginResult.AlreadyRegistered:
                error.Text = "Пользователь уже зарегистрирован";
                break;
        }
    }

    private void Auth_OnClick(object? sender, RoutedEventArgs e)
    {
        var register = new LoginWindowView();
        register.Show();
        ((Window)this.Parent).Close();
    }
}