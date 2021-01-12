using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDsl.Core
{
    public record SqlTable<T>(T Column1) where T : SqlColumnExpr;
    public record SqlTable<T1, T2>(T1 Column1, T2 Column2) where T1 : SqlColumnExpr
                                                           where T2 : SqlColumnExpr;

}
