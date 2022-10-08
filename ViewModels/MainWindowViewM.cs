using System.Reactive.Linq;
using System.Windows.Input;
using Microsoft.VisualBasic;
using ReactiveUI;

namespace InfoLab1.ViewModels
{
    public class MainWindowViewM : ViewModelBase , IReactiveObject
    {
        public string Greeting => "Welcome to Avalonia!";
        
        public Interaction< LoginWindowViewM?, MainWindowViewM> Interaction;

        public MainWindowViewM()
        {
            Interaction = new Interaction< LoginWindowViewM?, MainWindowViewM>();
            ICommand cmd = ReactiveCommand.CreateFromTask(async () =>
            {
                var login = new LoginWindowViewM();
                var result = await Interaction.Handle(login);
            });
        }


        public RoutingState Router { get; }
        
        
    }
}