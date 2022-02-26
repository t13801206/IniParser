using Prism.Mvvm;
using Reactive.Bindings;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System;
using System.Windows.Data;
using System.ComponentModel;

namespace IniFileUser.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public Models.Person Person { get; }
        
        public ReactiveCommand SaveButtonCommand { get; }

        public ReactiveCommand GreetingCommand { get; }

        public ObservableCollection<IniParser.Property> PropertyCollection { get; }

        public ReactiveProperty<string> FilterText { get; }

        private readonly ICollectionView _view;

        public MainWindowViewModel()
        {
            IniParser.Settings settings = new IniParser.Settings("./Settings.ini");
            Person = new Models.Person(settings["Person", "Name"], int.Parse(settings["Person", "Age"]));

            SaveButtonCommand = new ReactiveCommand()
                .WithSubscribe(() =>
                {
                    try
                    {
                        Person.Update(settings["Person", "Name"], int.Parse(settings["Person", "Age"]));
                        settings.Save();
                        Debug.WriteLine($"save succeeded !!");
                    }
                    catch (FormatException e)
                    {
                        Debug.WriteLine($"format exception !! {e.Message}");
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                });

            GreetingCommand = new ReactiveCommand()
                .WithSubscribe(Person.SayHello);

            PropertyCollection = new ObservableCollection<IniParser.Property>(settings.PropertyCollection);
            _view = CollectionViewSource.GetDefaultView(PropertyCollection);

            FilterText = new ReactiveProperty<string>("");
            FilterText.Subscribe(x =>
            {
                Debug.WriteLine(x);
                _view.Refresh();
            });

            _view.Filter = x =>
            {
                if (FilterText.Value == "")
                    return true;

                var property = x as IniParser.Property;
                //return property.Section.Contains(FilterText.Value);
                return property.Section.Contains(FilterText.Value) || property.Key.Contains(FilterText.Value) || property.Comment.Contains(FilterText.Value);
            };
        }
    }
}

// https://docs.microsoft.com/ja-jp/dotnet/desktop/wpf/controls/how-to-group-sort-and-filter-data-in-the-datagrid-control?view=netframeworkdesktop-4.8
// https://blog.okazuki.jp/entry/2014/10/29/220236
// https://increment.hatenablog.com/entry/2015/10/19/205222
