using Libreria.Application.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Libreria.Infrastructura
{
    public class DbSession
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession()
        {
            Connection = new SqlConnection(ConfigHelper.ConfigSqlServer!.Connection);
            Connection.Open();
            Connection.Close();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
