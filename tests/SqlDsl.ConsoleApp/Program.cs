using System;
using SqlDsl.Core;

namespace SqlDsl.ConsoleApp
{
    record Foo(string Name);
    
    class Program
    {


        static void Main(string[] args)
        {

            SqlExprInt leftSql = 1;
            SqlExprInt rightSql = 2;

            var sqlMult = leftSql / rightSql;

            var customers = (Table: "Customer", (Id: new SqlIntColumn("Id"), 
                                                 Age: new SqlIntColumn("Age")));
            var query = customers.From();
            
            Console.WriteLine(SqlCompiler.CompileOptimizedExpr(sqlMult));

        }
    }
}
