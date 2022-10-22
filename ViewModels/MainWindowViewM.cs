using System.Reactive.Linq;
using System.Windows.Input;
using Microsoft.VisualBasic;
using ReactiveUI;

namespace InfoLab1.ViewModels
{
    public class MainWindowViewM : ViewModelBase , IReactiveObject
    {
        public string Greeting => "Welcome to Avalonia!";
        

        public MainWindowViewM()
        {
            
        }

        
        
        
    }
}