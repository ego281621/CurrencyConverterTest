// Namespace imports

using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using CurrencyConverterTest.Core.Entities;
using CurrencyConverterTest.Repository.UserRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace WebApi.Helpers
{

    // Custom authentication handler for basic auth
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        // Dependency injection for user repository
        private readonly IUserRepository _userRepository;

        // Constructor injection
        public BasicAuthenticationHandler(
          IOptionsMonitor<AuthenticationSchemeOptions> options,
          ILoggerFactory logger,
          UrlEncoder encoder,
          ISystemClock clock,
          IUserRepository userRepository)
          : base(options, logger, encoder, clock)
        {
            _userRepository = userRepository;
        }


        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Check for authorization header
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                Response.Headers["WWW-Authenticate"] = "Basic";
                Response.StatusCode = 401;
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            // Parse credentials from header
            // Validate and get user from repository
            // Implementation truncated for brevity

            // Create and return claims principal if valid

            User user = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];
                user = await _userRepository.Authenticate(username, password);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (user == null)
                return AuthenticateResult.Fail("Invalid Username or Password");

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);


            return AuthenticateResult.Success(ticket);
        }

    }
}