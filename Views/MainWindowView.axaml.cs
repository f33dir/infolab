using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using InfoLab1.ViewModels;
using ReactiveUI;

namespace InfoLab1.Views
{
    public partial class MainWindowView : ReactiveWindow<MainWindowViewM>
    {
        public MainWindowView()
        {
            InitializeComponent();
        }
        
        
    }
}