using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ASP.Server.Models;

namespace ASP.Server.ViewModels
{
    public class EditBookViewModel
    {
        public int Id { get; set; } // Identifiant nécessaire pour la modification

        [Required]
        public string Title { get; set; } // Renommé de Name à Title pour correspondre au modèle Book

        [Required]
        public string Author { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        // Ajouter d'autres propriétés requises pour la modification
        // Par exemple, si vous voulez permettre la modification des genres associés au livre :
        public IEnumerable<int> SelectedGenreIds { get; set; }
        public IEnumerable<Genre> AllGenres { get; set; } // Pour afficher une liste de tous les genres disponibles

        // Vous pouvez également inclure d'autres propriétés telles que 'Content' si nécessaire
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}