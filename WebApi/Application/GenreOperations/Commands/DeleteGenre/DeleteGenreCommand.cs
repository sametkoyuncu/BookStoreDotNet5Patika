using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOprerations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int Id { get; set; }
        private readonly IBookStoreDbContext _context;
        public DeleteGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == Id);

            if (genre is null)
                throw new InvalidOperationException("Kitap Türü Bulunamadı!");

            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}