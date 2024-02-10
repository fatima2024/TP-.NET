using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ASP.Server.Models;
using ASP.Server.ViewModels;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace ASP.Server.Controllers
{
    public class BookController(LibraryDbContext libraryDbContext, IMapper mapper) : Controller
    {
        private readonly LibraryDbContext libraryDbContext = libraryDbContext;

        public ActionResult<IEnumerable<Book>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            IEnumerable<Book> ListBooks = libraryDbContext.Books.Include(b => b.Genres);
            return View(ListBooks);
        }


        public ActionResult Create(CreateBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Title = model.Name,
                    Author = model.Author,
                    Price = model.Price,
                    Content = model.Content,
                };

                // Ajouter les genres sélectionnés par l'utilisateur
                foreach (var genreId in model.Genres)
                {
                    var genre = libraryDbContext.Genres.Find(genreId);
                    if (genre != null)
                    {
                        book.Genres.Add(genre);
                    }
                }
                libraryDbContext.Books.Add(book);
                libraryDbContext.SaveChanges();

                return RedirectToAction(nameof(List)); // Rediriger vers la liste des livres après l'ajout
            }

            // Préparer de nouveau les genres pour le cas où la validation échoue

            return View(new CreateBookViewModel() { AllGenres = libraryDbContext.Genres.ToList() });

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var book = libraryDbContext.Books.Include(b => b.Genres).FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                libraryDbContext.Books.Remove(book);
                libraryDbContext.SaveChanges();
                return RedirectToAction("List");
            }

            return NotFound();
        }
        public ActionResult Edit(EditBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Recherche du livre par Id plutôt que par Title
                var book = libraryDbContext.Books
                    .Include(b => b.Genres)
                    .FirstOrDefault(b => b.Id == model.Id);

                if (book == null)
                {
                    return NotFound();
                }

                // Mise à jour des propriétés du livre
                book.Author = model.Author;
                book.Price = model.Price;

                // Mise à jour du titre uniquement si vous permettez la modification du titre
                // Si le titre peut être modifié, cela ne posera pas de problème ici
                book.Title = model.Title;

                // Mise à jour des genres du livre, si nécessaire
                // Assurez-vous de gérer correctement les relations de genres
                // Cela peut impliquer la suppression de toutes les relations existantes de genres
                // et l'ajout des nouvelles relations basées sur SelectedGenreIds

                libraryDbContext.SaveChanges();

                // Redirection vers la vue d'index ou toute vue appropriée après la mise à jour
                return RedirectToAction(nameof(List));
            }

            // Si le modèle n'est pas valide, rechargez la vue avec le modèle pour afficher les erreurs de validation
            // Assurez-vous de recharger les genres pour le modèle
            model.AllGenres = libraryDbContext.Genres.ToList();
            return View(model);
        }


    }
}