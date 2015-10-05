using System.Collections.Generic;
using DatabaseCopier.Library.Database;

namespace DatabaseCopier.Library.Interfaces
{
    public interface ITableSorter
    {
        IList<TableSource> Sort(IList<TableSource> tableSources);
    }
}