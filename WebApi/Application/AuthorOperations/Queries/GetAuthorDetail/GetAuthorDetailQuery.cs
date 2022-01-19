using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int Id { get; set; }

        public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.Where(x => x.Id == Id).SingleOrDefault();

            if (author is null)
                throw new InvalidOperationException("Yazar bulunamadı.");

            AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);

            return vm;

        }
    }

    public class AuthorDetailViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
    }
}