using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOprerations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int Id { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(g => g.Id == Id);
            if (genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı!");

            if (_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != Id))
                throw new InvalidOperationException("Kitap türü zaten mevcut!");

            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}