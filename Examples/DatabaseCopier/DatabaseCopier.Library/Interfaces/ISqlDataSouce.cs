using System.Collections.Generic;
using DatabaseCopier.Library.Database;
using DatabaseCopier.Library.Interfaces;

namespace DatabaseCopier.Library
{
    public interface ISqlDataSouce: ISqlDatabase
    {
        IList<TableSource> TableSources { get; set; }
    }
}