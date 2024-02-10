using System.Collections.Generic;

namespace ASP.Server.Dtos
{
    public class BookWithoutContentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<GenreDto> Genres { get; set; }
    }
}