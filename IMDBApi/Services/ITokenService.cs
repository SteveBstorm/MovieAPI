using IMDBApi.Models;

namespace IMDBApi.Services
{
    public interface ITokenService
    {
        UserWithToken Authenticate(string email, string password);
    }
}