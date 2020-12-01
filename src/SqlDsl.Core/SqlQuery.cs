using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SqlDsl.Core
{
    public interface SqlQuery { }
    public interface SqlQuery<T> : SqlQuery where T : ITuple
    {
    }

    public record FromClause(string Table, ITuple Columns) : SqlQuery;
    public record FromClause<T>(string Table, ITuple Columns) : FromClause(Table, Columns), SqlQuery<T> where T : ITuple;

    public record SelectClause(SqlQuery Query, Func<ITuple, ITuple> Mapf) : SqlQuery;
    public record SelectClause<T, R>(SqlQuery<T> TypedQuery, Func<T, R> MapF) : SelectClause(TypedQuery, tuple => MapF((T)tuple)), SqlQuery<R> 
        where T : ITuple
        where R : ITuple;
}
