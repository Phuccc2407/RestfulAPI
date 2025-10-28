using RestfulAPI.Repos;
using RestfulAPI.Repos.Models;
using RestfulAPI.Service.Interfaces;
using System.Security.Cryptography;

namespace RestfulAPI.Service.Implementations
{
    public class RefreshHandlerService : IRefreshHandlerService
    {
        private readonly LearndataContext context;
        public RefreshHandlerService(LearndataContext context) 
        {
            this.context = context;
        }
        public async Task<string> GenerateRefreshToken(string userId)
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                var refreshToken = Convert.ToBase64String(randomNumber);
                var existingToken = context.RefreshTokens.Find(userId);
                if (existingToken != null)
                {
                    existingToken.RefreshToken1 = refreshToken;
                    context.RefreshTokens.Update(existingToken);
                    existingToken.ExpiryDate = DateTime.Now.AddDays(7);
                }
                else
                {
                    var newToken = new RefreshToken
                    {
                        UserId = userId,
                        TokenId = new Random().Next().ToString(),
                        RefreshToken1 = refreshToken
                    };
                    context.RefreshTokens.Add(newToken);
                }
                await context.SaveChangesAsync();
                return refreshToken;
            }
        }
    }
}
