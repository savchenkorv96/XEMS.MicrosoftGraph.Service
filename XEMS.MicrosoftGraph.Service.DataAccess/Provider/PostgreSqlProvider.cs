using Npgsql;
using XEMS.MicrosoftGraph.Service.DataAccess.Contracts.Provider;

namespace XEMS.MicrosoftGraph.Service.DataAccess.Provider
{
    public class PostgreSqlProvider : IDbProvider<NpgsqlConnection>
    {
        public PostgreSqlProvider(string connectionString)
        {
            Connection = new NpgsqlConnection(connectionString);
        }

        public NpgsqlConnection Connection { get; }
    }
}