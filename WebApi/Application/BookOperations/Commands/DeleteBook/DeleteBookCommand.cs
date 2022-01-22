using System;
using System.Linq;
using WebApi.DBOperations;
namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public int Id { get; set; }
        public DeleteBookCommand(IBookStoreDbContext context)
        {
            _dbContext = context;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Id == Id);
            if (book is null)
                throw new InvalidOperationException("Kitap Bulunamadı!");

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}