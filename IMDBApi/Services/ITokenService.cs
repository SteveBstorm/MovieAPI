using IMDBApi.Models;

namespace IMDBApi.Services
{
    public interface ITokenService
    {
        ConnectedUser Authenticate(string email, string password);
    }
}