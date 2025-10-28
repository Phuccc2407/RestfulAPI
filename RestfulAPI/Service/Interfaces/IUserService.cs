using RestfulAPI.Modal;

namespace RestfulAPI.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserModal?> GetUserByIdAsync(string id);
        Task<bool> UploadAvatarAsync(IFormFile avatar, string userName);
    }
}
