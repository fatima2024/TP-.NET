using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ASP.Server.Models;

namespace ASP.Server.Database
{
    public class DbInitializer
    {
        public static void Initialize(LibraryDbContext bookDbContext)
        {
            // Vérifie si des livres existent déjà pour éviter de réinitialiser la base de données.
            if (bookDbContext.Books.Any())
                return;

            // Créez des genres avec des noms spécifiques.
            var sf = new Genre { Name = "Science Fiction" };
            var classic = new Genre { Name = "Classic" };
            var romance = new Genre { Name = "Romance" };
            var thriller = new Genre { Name = "Thriller" };

            // Ajoutez les genres à la base de données.
            bookDbContext.Genres.AddRange(sf, classic, romance, thriller);
            bookDbContext.SaveChanges();

            // Créez quelques exemples de livres avec les genres correspondants.
            var books = new List<Book>
    {
        new Book { Title = "Dune", Author = "Frank Herbert", Price = 9.99M, Genres = new List<Genre> { sf } },
        new Book { Title = "1984", Author = "George Orwell", Price = 8.99M, Genres = new List<Genre> { sf, classic } },
        new Book { Title = "Pride and Prejudice", Author = "Jane Austen", Price = 7.99M, Genres = new List<Genre> { classic, romance } },
        new Book { Title = "The Da Vinci Code", Author = "Dan Brown", Price = 6.99M, Genres = new List<Genre> { thriller } }
    };

            // Ajoutez les livres à la base de données.
            bookDbContext.Books.AddRange(books);
            bookDbContext.SaveChanges();
        }

    }
}