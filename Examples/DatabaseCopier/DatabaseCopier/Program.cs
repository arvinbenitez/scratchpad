using System;
using System.Collections.Generic;
using DatabaseCopier.Library.Database;
using DatabaseCopier.Library.Helper;

namespace DatabaseCopier.Library
{
    class Program
    {
        [Flags]
        enum Operation
        {
            CopySchema,
            CopyData
        }

        static void Main(string[] args)
        {
            //TODO: Which Operation?
            const Operation operation = Operation.CopyData | Operation.CopySchema;

            if (operation.HasFlag(Operation.CopySchema))
            {
                //TODO: Update the Database Settings
                const string serverName = @"ARVIN-DELL-PC\SQLEXPRESS";
                const string userId = "sa";
                const string password = "password";
                const string sourceDatabase = "AguaDeMaria";
                const string destinationDatabase = "Scratch";
                CopySchema(serverName, userId, password, sourceDatabase, destinationDatabase);
            }

            if (operation.HasFlag(Operation.CopyData))
            {
                //TODO: Update the Credentials
                var sourceDBCredentials = new DbCredentials
                {
                    DatabaseName = "AguaDeMaria",
                    ServerName = @"ARVIN-DELL-PC\SQLEXPRESS",
                    UserId = "sa",
                    Password = "password"
                };

                var destinationDBCredentials = new DbCredentials
                {
                    DatabaseName = "Scratch",
                    ServerName = @"ARVIN-DELL-PC\SQLEXPRESS",
                    UserId = "sa",
                    Password = "password"
                };

                var tableSource = Source.TableDefinitions();

                CopyData(sourceDBCredentials, destinationDBCredentials, tableSource);
            }
            Console.ReadLine();
        }

        #region Implementation
        private static void CopyData(DbCredentials sourceDatabase, DbCredentials destinationDatabase, IList<TableSource> tables)
        {
            var source = new SqlSqlDataSource(sourceDatabase.ServerName, sourceDatabase.DatabaseName,sourceDatabase.UserId, sourceDatabase.Password)
            {
                TableSources = tables
            };

            var destination = new SqlDatabase(destinationDatabase.ServerName, destinationDatabase.DatabaseName,
                destinationDatabase.UserId, destinationDatabase.Password);

            var copier = new SqlDataCopier(source, destination, new TableSorter());

            copier.OnProgress += (sender, eventArgs) => Console.WriteLine("Table: {0}, IsCompleted: {1}, Copied: {2}", eventArgs.TableName,
                eventArgs.IsCompleted, eventArgs.NumberOfRowsCopied);

            copier.OnError += (sender, exception) => Console.WriteLine("Error: {0}", exception);

            copier.Copy();
        }

        private static void CopySchema(string serverName, string userId, string password, string sourceDatabase, string destinationDatabase)
        {
            try
            {
                Console.WriteLine("Starting schema copy...");
                var schemaCopier = new SqlSchemaCopier(serverName, userId, password, sourceDatabase, destinationDatabase);
                schemaCopier.DataTransferEvent +=
                    (sender, args) =>
                        Console.WriteLine("Event Type: {0}, Message: {1}", args.DataTransferEventType, args.Message);
                schemaCopier.Start();
                Console.WriteLine("SUCESS! Completed schema copy.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("!!!Error encountered!!!:\n{0}", ex);
            }
        }
        #endregion
    }
}
