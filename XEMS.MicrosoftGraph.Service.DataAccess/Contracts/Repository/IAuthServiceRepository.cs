using System;
using System.Threading.Tasks;
using XEMS.MicrosoftGraph.Service.DataAccess.Domain.Entity;

namespace XEMS.MicrosoftGraph.Service.DataAccess.Contracts
{
    public interface IAuthServiceRepository
    {
        Task UpdateRefreshToken(int id, string refreshToken, DateTime refreshTokenExpiryTime);

        Task<AuthData> GetUserByСredentials(string login, string password);
        Task<AuthData> GetUserByRefreshToken(string refreshToken);
    }
}