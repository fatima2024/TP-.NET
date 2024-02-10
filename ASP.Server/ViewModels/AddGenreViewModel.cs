using System;
using System.ComponentModel.DataAnnotations;

namespace ASP.Server.ViewModels
{
    public class AddGenreViewModel
    {
        [Required(ErrorMessage = "Please enter the genre name.")]
        public string Name { get; set; }
    }
}
