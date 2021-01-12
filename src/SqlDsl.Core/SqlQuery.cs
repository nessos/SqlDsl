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

    public record FromClause(SqlTable Table) : SqlQuery;
    public record FromClause<T>(T FromTable) : FromClause(FromTable), SqlQuery<T> 
        where T : SqlTable;

    public record SelectClause(SqlQuery Query, Func<ITuple, ITuple> Mapf) : SqlQuery;
    public record SelectClause<T, R>(SqlQuery<T> TypedQuery, Func<T, R> MapF) : SelectClause(TypedQuery, tuple => MapF((T)tuple)), SqlQuery<R> 
        where T : ITuple
        where R : ITuple;
}
