using CurrencyConverterTest.Core.Entities;

namespace CurrencyConverterTest.Repository.UserRepository
{

    // Interface for user repository
    public interface IUserRepository
    {

        // Authenticates a user by username and password
        // Returns user object if valid credentials provided
        // Returns null if invalid credentials
        Task<User> Authenticate(string username, string password);

    }

}