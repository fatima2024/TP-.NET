using System.Collections.ObjectModel;
using MAUI.Reader.Model;

namespace MAUI.Reader.Service
{
    public class LibraryService
    {
        // A remplacer avec vos propre données !!!!!!!!!!!!!!
        // Pensé qu'il ne faut mieux ne pas réaffecter la variable Books, mais juste lui ajouter et / ou enlever des éléments
        // Donc pas de LibraryService.Instance.Books = ...
        // mais plutot LibraryService.Instance.Books.Add(...)
        // ou LibraryService.Instance.Books.Clear()
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>() {
            new Book(),
            new Book(),
            new Book()
        };

        // C'est aussi ici que vous ajouterez les requête réseau pour récupérer les livres depuis le web service que vous avez fait
        // Vous pourrez alors ajouter les livres obtenu a la variable Books !
        // Faite bien attention a ce que votre requête réseau ne bloque pas l'interface 
        public LibraryService()
        {
            Books = new ObservableCollection<Book>
    {
        new Book { Id = 1, Title = "Book 1", Author = "Author 1", Content = "Content 1", Price = 19.99m, Genres = "Fantasy" },
        new Book { Id = 2, Title = "Book 2", Author = "Author 2", Content = "Content 2", Price = 15.49m, Genres = "Science Fiction" },
        new Book { Id = 3, Title = "Book 3", Author = "Author 3", Content = "Content 3", Price = 8.99m, Genres = "Non-Fiction" },
        new Book { Id = 4, Title = "Book 4", Author = "Author 4", Content = "Content 4", Price = 12.95m, Genres = "Mystery" },
        new Book { Id = 5, Title = "Book 5", Author = "Author 5", Content = "Content 5", Price = 7.77m, Genres = "Romance" },
 
        // Ajoutez plus de livres ici selon vos besoins
    };
        }
        // Dans LibraryService.cs
        public ObservableCollection<Book> GetFirstNBooks(int n)
        {
            return new ObservableCollection<Book>(Books.Take(n));
        }


    }
}