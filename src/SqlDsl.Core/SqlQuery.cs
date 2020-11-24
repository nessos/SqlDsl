using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SqlDsl.Core
{
    public interface SqlQuery<T> where T : ITuple
    {
    }

    public record FromClause<T>(string Table, ITuple Columns) : SqlQuery<T> where T : ITuple; 

}
