using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace InfoLab1.Views;

public partial class TestUserControl : UserControl
{
    public TestUserControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}