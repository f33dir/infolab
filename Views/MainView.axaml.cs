using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using InfoLab1.Models;
using InfoLab1.Services;
using InfoLab1.ViewModels;

namespace InfoLab1.Views;

public partial class MainView : UserControl
{
    private MainViewM VM;
    private TextBox PasswordConrol;
    private TextBox UserPassword1c;
    private TextBox UserPassword2c;
    
    public MainView()
    {
        this.VM = new MainViewM();
        DataContext = VM;
        InitializeComponent();
        PasswordConrol = this.FindControl<TextBox>("Password");
        UserPassword1c = this.FindControl<TextBox>("UserPassword1");
        UserPassword2c = this.FindControl<TextBox>("UserPassword2");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void Users_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        User u ;
        if (e.AddedItems.Count > 0)
        {
            u = (User)e.AddedItems[0]!;
            VM.OnSelection(u);
        }

        PasswordConrol.Text = "";
    }

    private void SaveButton_OnClick(object? sender, RoutedEventArgs e)
    {
        VM.LoginService.UpdateUsers(VM.Users);
        VM.SelectedUser.Password = PasswordConrol.Text;
        VM.LoginService.SaveCredentials();
    }

    private void AddUserButton_OnClick(object? sender, RoutedEventArgs e)
    {
        VM.AddUser(new User("New User",null,false ,true));
        VM.LoginService.SaveCredentials();
    }

    private void DeleteUserButton_OnClick(object? sender, RoutedEventArgs e)
    {
        VM.Users.Remove(VM.SelectedUser);
        VM.LoginService.SaveCredentials();
    }

    private void ChangePassword_OnClick(object? sender, RoutedEventArgs e)
    {
        var res = VM.LoginService.UpdatePassword(VM.CurrentUser.Username, UserPassword1c.Text, UserPassword2c.Text);
        switch (res)
        {
            case LoginResult.Success:
                
                break;
        }
    }
}