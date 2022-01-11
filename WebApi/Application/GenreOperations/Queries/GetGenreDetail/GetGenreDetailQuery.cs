using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOprerations.Queries.GenreDetail
{
    public class GenreDetailQuery
    {
        public int Id { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == Id);
            if (genre is null)
                throw new InvalidOperationException("Kitap türü Bulunamadı");
            return _mapper.Map<GenreDetailViewModel>(genre);
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}