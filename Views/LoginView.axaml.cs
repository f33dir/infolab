using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Mixins;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using InfoLab1.Services;
using InfoLab1.ViewModels;
using ReactiveUI;

namespace InfoLab1.Views;
public partial class LoginView : ReactiveUserControl<LoginViewM>
{
    private LoginViewM VM;
    public TextBlock error;
    public LoginView()
    {
        this.VM = new LoginViewM();
        DataContext = VM;
        InitializeComponent();
        error = this.FindControl<TextBlock>("Error");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Auth_OnClick(object? sender, RoutedEventArgs e)
    {
        switch (VM.DoLogin()){
            case LoginResult.NoPassword:
                error.Text = "Необходимо завершить регистрацию в  системе на странице регистрации";
                break;
            case LoginResult.WrongPassword:
                error.Text = "Неправильный пароль";
                break;
            case LoginResult.NoUser:
                error.Text = "Пользователь не найден";
                break;
            case LoginResult.Success:
                ShowMainWindow();
                break;
        }
    }

    private void Register_OnClick(object? sender, RoutedEventArgs e)
    {
        var register = new RegisterWindowView();
        register.Show();
        ((Window)this.Parent).Close();
    }

    private void ShowMainWindow()
    {
        var w = new MainWindow();
        w.Show();
        ((Window)this.Parent).Close();
    }
}