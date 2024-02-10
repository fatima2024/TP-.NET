
using CommunityToolkit.Mvvm.DependencyInjection;

using CommunityToolkit.Mvvm.Input;

using System.Collections.ObjectModel;

using System.ComponentModel;

using MAUI.Reader.Model;

using MAUI.Reader.Service;

using System.Windows.Input;

using CommunityToolkit.Maui.Alerts;
 
namespace MAUI.Reader.ViewModel

{

    public partial class ListBooks : INotifyPropertyChanged

    {

        private string _title = "Titre par défaut"; // Valeur par défaut pour le titre

        public ListBooks()

        {

            ItemSelectedCommand = new Command(OnItemSelectedCommand);

        }

        public ICommand ItemSelectedCommand { get; private set; }

        // Propriété Title avec notification de changement

        public string Title

        {

            get => _title;

            set

            {

                if (_title != value)

                {

                    _title = value;

                    OnPropertyChanged(nameof(Title)); // Notifie que la propriété Title a changé

                }

            }

        }

        // Méthode appelée lorsqu'un élément est sélectionné

        public void OnItemSelectedCommand(object book)

        {

            if (book is Book selectedBook)

            {

                Ioc.Default.GetRequiredService<INavigationService>().Navigate<DetailsBook>(selectedBook);

            }

        }

        // Collection des livres pour le binding

        public ObservableCollection<Book> Books => Ioc.Default.GetRequiredService<LibraryService>().Books;

        // Compteur pour une action spécifique (exemple donné, peut être modifié selon les besoins)

        public int Count { get; set; }

        [RelayCommand]

        public void CounterClicked()

        {

            Count++;

            Ioc.Default.GetRequiredService<INavigationService>().Navigate<DetailsBook>(new Book());

        }

        // Implémentation de INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)

        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }


    }

}