using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class BaseConnection
    {
        private static string conn_str = ConfigurationManager.ConnectionStrings["DBSQLConn"].ConnectionString;
        protected SqlConnection GetOpenConnection(string ConnName = null)
        {
            string connString = string.Empty;
            connString = ConnName == null ? connString = conn_str : ConfigurationManager.ConnectionStrings[ConnName].ConnectionString;
            var connection = new SqlConnection(connString);
            connection.Open();
            return connection;
        }
    }
}