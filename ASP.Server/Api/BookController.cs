using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.Dtos; 
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;

namespace ASP.Server.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BookController(LibraryDbContext context)
        {
            _context = context;

        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var bookDto = await _context.Books
                .Where(b => b.Id == id)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Name = b.Title,
                    Price = b.Price,
                    Content = b.Content,
                    Genres = b.Genres.Select(g => new GenreDto { Id = g.Id, Name = g.Name }).ToList()
                })
                .FirstOrDefaultAsync();

            if (bookDto == null)
            {
                return NotFound();
            }

            return bookDto;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookWithoutContentDto>>> GetBooks()
        {
            var books = await _context.Books
                .Select(b => new BookWithoutContentDto
                {
                    Id = b.Id,
                    Name = b.Title,
                    Price = b.Price,
                    Genres = b.Genres.Select(g => new GenreDto { Id = g.Id, Name = g.Name }).ToList()
                })
                .ToListAsync();

            return Ok(books);
        }

        // ... Other actions ...
    }

}