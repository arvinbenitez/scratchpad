using System;

namespace DatabaseCopier.Library.Events
{
    public class DataCopiedEventArgs : EventArgs
    {
        public DataCopiedEventArgs(string tableName, bool isCompleted, long numberOfRowsCopied)
        {
            TableName = tableName;
            IsCompleted = isCompleted;
            NumberOfRowsCopied = numberOfRowsCopied;
        }

        public string TableName { get; set; }
        public bool IsCompleted { get; set; }
        public long NumberOfRowsCopied { get; set; }
    }

}
