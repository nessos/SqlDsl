using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SqlDsl.Core
{
    public record SqlTable(string TableName) : ITuple
    {
        public object this[int index] => throw new NotImplementedException();

        public int Length => throw new NotImplementedException();
    }
    public record SqlTable<T>(string TableName, T Column1) : SqlTable(TableName) where T : SqlColumnExpr;
    public record SqlTable<T1, T2>(string TableName, T1 Column1, T2 Column2) : SqlTable(TableName)
            where T1 : SqlColumnExpr
            where T2 : SqlColumnExpr;

}
