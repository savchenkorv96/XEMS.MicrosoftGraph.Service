using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.DataAccess.Contracts;
using XEMS.MicrosoftGraph.Service.DataAccess.Contracts.Provider;
using XEMS.MicrosoftGraph.Service.DataAccess.Domain.Entity;

namespace XEMS.MicrosoftGraph.Service.DataAccess.Repository
{
    public class AuthServiceRepository : IAuthServiceRepository
    {
        private readonly IDbProvider<NpgsqlConnection> connection;
        private readonly Logger logger;

        public AuthServiceRepository(IDbProvider<NpgsqlConnection> connection, Logger logger)
        {
            this.connection = connection;
            this.logger = logger;
        }

        public async Task UpdateRefreshToken(int id, string refreshToken, DateTime refreshTokenExpiryTime)
        {
            var sql =
                "UPDATE authdata SET refreshtoken = @RefreshToken, refreshtokenexpirytime = @RefreshTokenExpiryTime WHERE Id = @Id";

            try
            {
                var updateQuery = await connection.Connection.ExecuteAsync(sql, new
                {
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = refreshTokenExpiryTime,
                    Id = id
                });

                logger.Information($"Update RefreshToken for id {id} successfully.");
            }
            catch (Exception e)
            {
                logger.Error($"Update RefreshToken for id {id} unsuccessfully Message: {e.Message}");
                throw;
            }
        }

        public async Task<AuthData> GetUserByСredentials(string login, string password)
        {
            var sql = "SELECT * FROM authdata a WHERE a.login = @Login AND a.password = @Password";
            AuthData result = null;

            try
            {
                result = connection.Connection.QueryAsync<AuthData>(sql, new {Login = login, Password = password})
                    .Result.Distinct().First();


                logger.Information($"GetUserByСredentials for login {login} successfully.");
            }
            catch (Exception e)
            {
                logger.Error($"GetUserByСredentials for login {login} unsuccessfully. Message: {e.Message}");
                throw;
            }

            return result;
        }

        public async Task<AuthData> GetUserByRefreshToken(string refreshToken)
        {
            var sql = "SELECT * FROM authdata a WHERE a.refreshToken = @RefreshToken";

            AuthData result = null;

            try
            {
                result = connection.Connection.QueryAsync<AuthData>(sql, new {RefreshToken = refreshToken}).Result
                    .Distinct().First();

                logger.Information($"GetUserByRefreshToken for refreshToken {refreshToken} successfully.");
            }
            catch (Exception e)
            {
                logger.Error($"GetUserByRefreshToken for refreshToken {refreshToken} unsuccessfully Message: {e.Message}");
                throw;
            }

            return result;
        }

        public async Task AddService(AuthData item)
        {
            var sql =
                "INSERT INTO authdata(serviceAccessLevel, serviceName, serviceDescription, login, password, isActive, refreshToken, refreshTokenExpiryTime) " +
                "VALUES(@ServiceAccessLevel, @ServiceName, @ServiceDescription, @Login, @Password, @IsActive, @RefreshToken, @RefreshTokenExpiryTime);";

            try
            {
                var insertQuery = await connection.Connection.ExecuteAsync(sql, new
                {
                    item.ServiceAccessLevel,
                    item.ServiceName,
                    item.ServiceDescription,
                    item.Login,
                    item.Password,
                    item.IsActive,
                    item.RefreshToken,
                    item.RefreshTokenExpiryTime
                });

                logger.Information($"AddService with login {item.Login} successfully.");
            }
            catch (Exception e)
            {
                logger.Error($"AddService with login {item.Login} unsuccessfully Message: {e.Message}");
                throw;
            }
        }
    }
}