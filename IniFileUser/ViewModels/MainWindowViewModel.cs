using Prism.Mvvm;
using Reactive.Bindings;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System;

namespace IniFileUser.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ReactiveCommand SaveButtonCommand { get; }

        public ReactiveCommand GreetingCommand { get; }

        public ObservableCollection<IniParser.Property> CategoryViewSource { get; }

        public Models.Person Person { get; }

        public MainWindowViewModel()
        {
            IniParser.Settings settings = new IniParser.Settings("./Settings.ini");

            Person = new Models.Person(settings["Person", "Name"], int.Parse(settings["Person", "Age"]));

            SaveButtonCommand = new ReactiveCommand();
            SaveButtonCommand.Subscribe(() =>
            {
                try
                {
                    Person.Update(settings["Person", "Name"], int.Parse(settings["Person", "Age"]));
                    settings.Save();
                    Debug.WriteLine($"save succeeded !!");
                }
                catch(FormatException e)
                {
                    Debug.WriteLine($"format exception !! {e.Message}");
                }
                catch(Exception e)
                {
                    throw e;
                }
            });

            GreetingCommand = new ReactiveCommand();
            GreetingCommand.Subscribe(Person.SayHello);

            CategoryViewSource = new ObservableCollection<IniParser.Property>(settings.PropertyCollection);
        }
    }
}
