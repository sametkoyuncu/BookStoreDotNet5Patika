using System;
using System.Linq;
using WebApi.DBOperations;
namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int Id { get; set; }
        public UpdateBookModel Model = new UpdateBookModel();
        public UpdateBookCommand(BookStoreDbContext context)
        {
            _dbContext = context;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Id == Id);
            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");
            // gelen veri default değer mi? yani boş mu gelmiş?
            // eğer öyle değilse geleni, öyleyse eski veriyi yaz
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.AuthorId = Model.AuthorId != default ? Model.AuthorId : book.AuthorId;
            book.Title = Model.Title != default ? Model.Title : book.Title;

            _dbContext.SaveChanges();
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
    }
}