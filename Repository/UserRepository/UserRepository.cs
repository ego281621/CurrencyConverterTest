using CurrencyConverterTest.Core.Entities;
using CurrencyConverterTest.CustomException;
using CurrencyConverterTest.Models;
using CurrencyConverterTest.Repository.RequestLogRepository;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;

namespace CurrencyConverterTest.Repository.UserRepository
{

    // Implements IUserRepository interface
    public class UserRepository : IUserRepository
    {

        // Dependency injection for database context
        private readonly CurrencyTestDbContext _context;

        // Constructor injects database context
        public UserRepository(CurrencyTestDbContext context)
        {
            this._context = context;
        }

        // Implementation of Authenticate method
        public async Task<User> Authenticate(string username, string password)
        {
            // Query database for user with matching credentials 
            var user = await Task.Run(() => _context.Users.SingleOrDefault(x => x.UserName == username && x.Password == password));

            // If no user found, return null
            if (user == null)
                return null;

            // Return user object
            return user;
        }

    }

}