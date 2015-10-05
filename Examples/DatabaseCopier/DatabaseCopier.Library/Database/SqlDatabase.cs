using System.Data.SqlClient;
using DatabaseCopier.Library.Interfaces;

namespace DatabaseCopier.Library.Database
{
    public class SqlDatabase : ISqlDatabase
    {
        private readonly string serverName;
        private readonly string databaseName;
        private readonly string userId;
        private readonly string password;

        const string connectionStringFormat = @"data source={0};initial catalog={1};persist security info=True;user id={2};password={3};MultipleActiveResultSets=True;";

        public SqlDatabase(string serverName, string databaseName, string userId, string password)
        {
            this.serverName = serverName;
            this.databaseName = databaseName;
            this.userId = userId;
            this.password = password;
        }

        public SqlConnection OpenConnection()
        {
            var connectionString = string.Format(connectionStringFormat, serverName, databaseName, userId, password);
            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }
    }
}