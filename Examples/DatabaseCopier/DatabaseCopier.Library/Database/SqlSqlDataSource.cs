using System.Collections.Generic;

namespace DatabaseCopier.Library.Database
{
    public class SqlSqlDataSource :SqlDatabase, ISqlDataSouce
    {
        public SqlSqlDataSource(string serverName, string databaseName, string userId, string password): base(serverName,databaseName,userId,password)
        {
            TableSources = new List<TableSource>();
        }

        public IList<TableSource> TableSources { get; set; }
    }
}