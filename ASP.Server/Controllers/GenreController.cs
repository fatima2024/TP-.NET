using Microsoft.AspNetCore.Mvc;
using ASP.Server.Database;
using ASP.Server.ViewModels;
using ASP.Server.Models;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;

namespace ASP.Server.Controllers
{
    public class GenreController : Controller
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly IMapper _mapper;

        public GenreController(LibraryDbContext libraryDbContext, IMapper mapper)
        {
            _libraryDbContext = libraryDbContext;
            _mapper = mapper;
        }

        public ActionResult<IEnumerable<Genre>> List()
        {
            var genres = _libraryDbContext.Genres.ToList();
            return View(genres);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddGenreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var genre = new Genre { Name = model.Name };
                _libraryDbContext.Genres.Add(genre);
                _libraryDbContext.SaveChanges();
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }
    }
}
