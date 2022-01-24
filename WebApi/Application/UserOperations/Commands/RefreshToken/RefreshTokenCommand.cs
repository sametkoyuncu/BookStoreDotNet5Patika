using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;
        public RefreshTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);

            if (user is null) throw new InvalidOperationException("Geçerli bir refresh token bulunamadı!");

            //  Create Token
            TokenHandler handler = new TokenHandler(_configuration);

            Token token = handler.CreateAccessToken(user);

            user.RefreshToken = token.RefreshToken;
            // user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
            user.RefreshTokenExpireDate = token.Expiration;

            _context.SaveChanges();
            return token;
        }
    }
}