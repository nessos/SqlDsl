using System;
using SqlDsl.Core;

namespace SqlDsl.ConsoleApp
{
    
    class Program
    {
        static void Main(string[] args)
        {

            Func<SqlExprInt, SqlExprInt> f = x => 1 + x + 1 + 1 + 1 + 1;
            
            var expr = f(1);



            Console.WriteLine(SqlCompiler.CompileExpr(expr));

        }
    }
}
