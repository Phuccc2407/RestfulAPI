namespace RestfulAPI.Service.Interfaces
{
    public interface IAdminService
    {
        Task<int> GetTotalUsersAsync();
        Task<int> GetTotalTracksAsync();
    }
}
