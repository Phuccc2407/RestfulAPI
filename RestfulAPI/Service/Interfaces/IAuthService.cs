using RestfulAPI.Modal;

namespace RestfulAPI.Service.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterUser(LoginModal user);
    }
}