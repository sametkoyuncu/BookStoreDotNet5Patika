using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>()
        {
            new Book {
                Id = 1,
                Title = "Lean Startup",
                GenreId = 1,
                PageCount = 200,
                PublishDate = new DateTime(2001,02,06)
            },
            new Book {
                Id = 2,
                Title = "Herland",
                GenreId = 2,
                PageCount = 250,
                PublishDate = new DateTime(2010,03,12)
            },
            new Book {
                Id = 3,
                Title = "Dune",
                GenreId = 2,
                PageCount = 250,
                PublishDate = new DateTime(2011,12,12)
            }
        };

        [HttpGet]
        public List<Book> GetBooks()
        {
            var booklist = BookList.OrderBy(x => x.Id).ToList<Book>();
            return booklist;
        }

        [HttpGet]
        [Route("{id}")]
        public Book GetBookById(int id)
        {
            var book = BookList.Where(x => x.Id == id).SingleOrDefault();
            return book;
        }

        // [HttpGet]
        // public Book GetBookByIdFromQuery([FromQuery] string id)
        // {
        //     var book = BookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }
    }
}