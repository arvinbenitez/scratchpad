using System.Data.SqlClient;

namespace DatabaseCopier.Library.Interfaces
{
    public interface ISqlDatabase
    {
        SqlConnection OpenConnection();
    }
}