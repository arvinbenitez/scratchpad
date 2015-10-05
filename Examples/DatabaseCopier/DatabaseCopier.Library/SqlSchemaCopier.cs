using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DatabaseCopier.Library
{
    /// <summary>
    /// Copies the Schema of SQL Database from  one database to another on the same server.
    /// NOTE: This only works if the source and target are on the same server.
    /// </summary>
    public class SqlSchemaCopier
    {
        private readonly string serverName;
        private readonly string userId;
        private readonly string password;
        private readonly string sourceDatabaseName;
        private readonly string destinationDatabaseName;

        public event DataTransferEventHandler DataTransferEvent;

        public SqlSchemaCopier(string serverName, string userId, string password, string sourceDatabaseName, string destinationDatabaseName)
        {
            this.serverName = serverName;
            this.userId = userId;
            this.password = password;
            this.sourceDatabaseName = sourceDatabaseName;
            this.destinationDatabaseName = destinationDatabaseName;
        }

        public void Start()
        {
            var connection = new ServerConnection(serverName, userId, password);
            var server = new Server(connection);

            var database = server.Databases[sourceDatabaseName];

            var transfer = new Transfer(database)
            {
                CopyAllObjects = true,
                Options = { WithDependencies = true },
                DestinationDatabase = destinationDatabaseName,
                DestinationServer = serverName,
                DestinationPassword = password,
                DestinationLogin = userId,
                DestinationLoginSecure = false,
                CopySchema = true,
                CopyData = false,
                DropDestinationObjectsFirst = true
            };
            transfer.Options.ContinueScriptingOnError = true;
            transfer.DataTransferEvent += TransferOnDataTransferEvent;
            transfer.TransferData();
        }

        private void TransferOnDataTransferEvent(object sender, DataTransferEventArgs e)
        {
            if (DataTransferEvent != null)
            {
                DataTransferEvent(this, e);
            }
        }
    }
}
