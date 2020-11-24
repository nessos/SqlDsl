using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SqlDsl.Core
{
    public static class SqlQueryExtensions
    {
        public static SqlQuery<T> From<T>(this (string Table, T Columns) table) where T : ITuple
        {
            return new FromClause<T>(table.Table, table.Columns);
        }
    }
}
