using System;
using System.Collections.Generic;
using System.Diagnostics;
using DatabaseCopier.Library.Database;
using DatabaseCopier.Library.Interfaces;

namespace DatabaseCopier.Library.Helper
{
    public class TableSorter : ITableSorter, IComparer<TableSource>
    {
        public IList<TableSource> Sort(IList<TableSource> tableSources)
        {
            var sortedList = new List<TableSource>(tableSources);
            sortedList.Sort(this);
            return sortedList;
        }

        public int Compare(TableSource x, TableSource y)
        {
            if (y.TableDependencies.Contains(x.Name))            
            {
                Debug.WriteLine(string.Format("(x) {1} is a dependency of (y) {0}. Returning -1", y.Name, x.Name));
                return -1;
            }

            if (x.TableDependencies.Contains(y.Name))
            {
                Debug.WriteLine(string.Format("(x) {0} has a dependency on (y) {1}. Returning 1", x.Name, y.Name));
                return 1;
            }

            if (x.TableDependencies.Count > 0 && y.TableDependencies.Count == 0)
            {
                //those without dependencies should get be on top
                Debug.WriteLine(string.Format("(x) {0} has a dependency while (y) {1} does not. Returning 1", x.Name, y.Name));
                return 1;
            }

            if (x.TableDependencies.Count == 0 && y.TableDependencies.Count > 0)
            {
                //those without dependencies should get be on top
                Debug.WriteLine(string.Format("(x) {0} has no dependency while (y) {1} has. Returning -1", x.Name, y.Name));
                return -1;
            }

            var result = String.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
            Debug.WriteLine(string.Format("(x) {0} vs (y) {1}. Returning {2}", x.Name, y.Name, result));
            return result;
        }
    }
}