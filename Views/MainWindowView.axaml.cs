using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using InfoLab1.ViewModels;
using ReactiveUI;

namespace InfoLab1.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewM>
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task DoShowDialogAsync(InteractionContext<LoginWindowViewM, MainWindowViewM?> interaction)
        {
            var dialog = new LoginWindowView();
            dialog.DataContext = interaction.Input;

            var res = await dialog.ShowDialog<MainWindowViewM?>(this);
            interaction.SetOutput(res);
        }
    }
}