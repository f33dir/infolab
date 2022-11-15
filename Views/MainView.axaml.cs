using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using InfoLab1.Models;
using InfoLab1.ViewModels;

namespace InfoLab1.Views;

public partial class MainView : UserControl
{
    private MainViewM VM;
    private TextBox PasswordConrol;

    public MainView()
    {
        this.VM = new MainViewM();
        DataContext = VM;
        InitializeComponent();
        PasswordConrol = this.FindControl<TextBox>("Password");
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
}