using Prism.Mvvm;
using Reactive.Bindings;
using System.Collections.Generic;
using System.Windows.Data;

namespace IniFileUser.ViewModels
{
    // https://www.sach.codes/c%23/wpf/mvvm/collectionview/filtering/2019/12/19/Filter-Data-Using-CollectionView.html

    public class SampleProgramViewModel : BindableBase
    {
        public ReactiveCommand FilterButtonClicked { get; }

        public ReactiveProperty<string> TbFrom { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> TbTo { get; } = new ReactiveProperty<string>();

        public List<Contact> Contacts { get; }

        public CollectionView ContactFilterView { get; }

        public SampleProgramViewModel()
        {
            FilterButtonClicked = new ReactiveCommand().WithSubscribe(FilterData);
            Contacts = GetContacts();
            ContactFilterView = (CollectionView)CollectionViewSource.GetDefaultView(Contacts);
            ContactFilterView.Filter = OnFilterTriggered;
        }

        private void FilterData()
        {
            CollectionViewSource.GetDefaultView(Contacts).Refresh();
        }

        private bool OnFilterTriggered(object item)
        {
            if (item is Contact contact)
            {
                var bFrom = int.TryParse(TbFrom.Value, out int from);
                var bTo = int.TryParse(TbTo.Value, out int to);

                if (bFrom && bTo)
                    return contact.Age >= from && contact.Age <= to;
            }

            return true;
        }

        private List<Contact> GetContacts()
        {
            return new List<Contact>
            {
                new Contact() { Age = 33, Name = "Chelsea" },
                new Contact() { Age = 30, Name = "Taylor" },
                new Contact() { Age = 35, Name = "Chris" },
                new Contact() { Age = 23, Name = "Scarlett" },
                new Contact() { Age = 42, Name = "Dwayne" },
            };
        }
    }

    public class Contact
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
