using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int Id { get; set; }
        public GetBookDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Include(x => x.Genre).Include(x => x.Author).Where(x => x.Id == Id).SingleOrDefault();
            if (book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı!");
            }
            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);
            // BookDetailViewModel vm = new BookDetailViewModel();
            // vm.Title = book.Title;
            // vm.PageCount = book.PageCount;
            // vm.Genre = ((GenreEnum)book.GenreId).ToString();
            // vm.PublishDate = book.PublishDate.Date.ToString("dd/mmm/yyyy");
            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}