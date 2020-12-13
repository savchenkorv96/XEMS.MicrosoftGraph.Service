using System.Data.Common;

namespace XEMS.MicrosoftGraph.Service.DataAccess.Contracts.Provider
{
    public interface IDbProvider<T> where T : DbConnection
    {
        T Connection { get; }
    }
}