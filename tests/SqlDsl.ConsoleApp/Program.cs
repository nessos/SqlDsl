using System;
using SqlDsl.Core;

namespace SqlDsl.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Func<SqlExprInt, SqlExprInt> f = x => x + 1;
            
            var expr = f(1);
            
            

            Console.WriteLine(SqlCompiler.CompileExpr(expr));
        }
    }
}
