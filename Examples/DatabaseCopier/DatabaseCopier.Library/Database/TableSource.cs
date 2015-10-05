using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DatabaseCopier.Library.Database
{
    public class TableSource
    {
        public TableSource()
        {
            TableDependencies = new List<string>();
        }

        public static TableSource ToTableSource(string json)
        {
            try
            {
                var tableSource = JsonConvert.DeserializeObject<TableSource>(json);
                return tableSource;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static TableSource ToTableSource(string tableName, string filter, params string[] dependencies)
        {
            var tableSource = new TableSource
            {
                Name = tableName,
                Filter = filter
            };
            if (dependencies != null && dependencies.Length > 0)
            {
                tableSource.TableDependencies = new List<string>(dependencies);
            }
            return tableSource;
        }


        public string Name { get; set; }

        public IList<string> TableDependencies { get; set; }

        public string Filter { get; set; }

        public override string ToString()
        {
            var sql = "SELECT * FROM [" + Name + "]" + (string.IsNullOrEmpty(Filter) ? string.Empty : "WHERE " + Filter);
            return sql;
        }
    }
}