using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Book> GetBooks()
        {
            var booklist = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            return booklist;
        }

        [HttpGet]
        [Route("{id}")]
        public Book GetBookById(int id)
        {
            var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
            return book;
        }

        // [HttpGet]
        // public Book GetBookByIdFromQuery([FromQuery] string id)
        // {
        //     var book = _context.Books.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(b => b.Title == newBook.Title);
            if (book is not null)
                return BadRequest();

            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == updatedBook.Id);
            if (book is null)
                return BadRequest();
            // gelen veri default değer mi? yani boş mu gelmiş?
            // eğer öyle değilse geleni, öyleyse eski veriyi yaz
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id);
            if (book is null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}