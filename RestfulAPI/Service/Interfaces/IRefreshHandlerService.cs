namespace RestfulAPI.Service.Interfaces
{
    public interface IRefreshHandlerService
    {
        Task<string> GenerateRefreshToken(string userId);
    }
}
