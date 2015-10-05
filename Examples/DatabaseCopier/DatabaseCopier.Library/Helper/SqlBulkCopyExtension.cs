using System;
using System.Data.SqlClient;
using System.Reflection;

namespace DatabaseCopier.Library.Helper
{
    public static class SqlBulkCopyExtension
    {
        const String RowsCopiedFieldName = "_rowsCopied";
        static FieldInfo rowsCopiedField;

        public static int RowsCopiedCount(this SqlBulkCopy bulkCopy)
        {
            if (rowsCopiedField == null)
                rowsCopiedField = typeof (SqlBulkCopy).GetField(RowsCopiedFieldName,
                    BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);

            if (rowsCopiedField != null) return (int) rowsCopiedField.GetValue(bulkCopy);
            return 0;
        }
    }
}
