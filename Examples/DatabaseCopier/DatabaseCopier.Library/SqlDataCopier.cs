using System;
using System.Data.SqlClient;
using DatabaseCopier.Library.Events;
using DatabaseCopier.Library.Interfaces;
using DatabaseCopier.Library.Helper;

namespace DatabaseCopier.Library
{
    public class SqlDataCopier
    {
        private readonly ISqlDataSouce dataSouce;
        private readonly ISqlDatabase destinationDatabase;
        private readonly ITableSorter tableSorter;

        private const int NotifyAfterNumberOfRows = 10000;

        public delegate void DataCopiedEventHandler(object sender, DataCopiedEventArgs e);

        public delegate void DataCopyExceptionEventHander(object sender, Exception ex);

        public event DataCopiedEventHandler OnProgress;
        public event DataCopyExceptionEventHander OnError;

        public SqlDataCopier(ISqlDataSouce dataSouce, ISqlDatabase destinationDatabase, ITableSorter tableSorter)
        {
            this.dataSouce = dataSouce;
            this.destinationDatabase = destinationDatabase;
            this.tableSorter = tableSorter;
        }

        private long totalRowsCopied;

        public void Copy()
        {
            SqlConnection source = null;
            SqlConnection destination = null;

            try
            {
                source = dataSouce.OpenConnection();
                destination = destinationDatabase.OpenConnection();
                var tableSources = tableSorter == null
                    ? dataSouce.TableSources
                    : tableSorter.Sort(dataSouce.TableSources);

                foreach (var tableSource in tableSources)
                {
                    totalRowsCopied = 0;
                    RaiseEvent(tableSource.Name, false, totalRowsCopied);
                    BulkCopy(tableSource.Name, tableSource.ToString(), source, destination);
                }
            }
            catch (Exception e)
            {
                if (OnError != null)
                {
                    OnError(this, e);
                }
            }
            finally
            {
                if (source != null)
                {
                    source.Close();
                }
                if (destination != null)
                {
                    destination.Close();
                }
            }
        }

        private void RaiseEvent(string tableName, bool isCompleted, long rowsCopied)
        {
            totalRowsCopied += rowsCopied;
            if (OnProgress != null)
            {
                OnProgress(this, new DataCopiedEventArgs(tableName, isCompleted, totalRowsCopied));
            }
        }

        private void BulkCopy(string tableName, string sql, SqlConnection sourceConnection, SqlConnection targetConnection)
        {
            using (var cmd = new SqlCommand(sql, sourceConnection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    TransferData(reader, tableName, targetConnection);
                }
            }
        }

        private void TransferData(SqlDataReader reader, string tableName, SqlConnection targetConnection)
        {
            using (var bulkCopy = new SqlBulkCopy(targetConnection, SqlBulkCopyOptions.KeepIdentity, null))
            {
                bulkCopy.SqlRowsCopied += SqlRowsCopied;
                bulkCopy.NotifyAfter = NotifyAfterNumberOfRows;
                bulkCopy.DestinationTableName = "[" + tableName + "]";
                bulkCopy.WriteToServer(reader);

                RaiseEvent(tableName, true, bulkCopy.RowsCopiedCount());
            }
        }

        private void SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            RaiseEvent(
                ((SqlBulkCopy) sender).DestinationTableName.Replace("[", string.Empty).Replace("]", string.Empty), false,
                e.RowsCopied);
        }
    }
}