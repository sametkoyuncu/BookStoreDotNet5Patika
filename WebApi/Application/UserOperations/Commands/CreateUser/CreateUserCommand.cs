using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == Model.UserName || x.Email == Model.Email);
            if (user is not null)
                throw new InvalidOperationException("Kullanıcı zaten mevcut!");

            user = _mapper.Map<User>(Model);

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }

    public class CreateUserModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}