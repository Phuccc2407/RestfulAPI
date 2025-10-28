using RestfulAPI.Modal;
using RestfulAPI.Repos.Models;

namespace RestfulAPI.Service.Interfaces
{
    public interface IAuthService
    {
        //Task<string> GenerateTokenString(User user);
        Task<bool> LoginWithCookie(LoginModal user);
        Task<bool> RegisterUser(RegisterModal user);
        Task Logout();
    }
}