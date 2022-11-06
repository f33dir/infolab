using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using InfoLab1.Models;
using InfoLab1.ViewModels;

namespace InfoLab1.Views;

public partial class MainView : UserControl
{
    private MainViewM VM;

    public MainView()
    {
        this.VM = new MainViewM();
        DataContext = VM;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Users_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        VM.OnSelection((User)e.AddedItems[0]);
    }
}