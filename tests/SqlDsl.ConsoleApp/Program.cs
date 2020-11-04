using System;
using SqlDsl.Core;

namespace SqlDsl.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlExpr<SqlInt> first = new SqlIntValue(1);
            SqlExpr<SqlInt> second = new SqlIntValue(3);
            var add = new SqlIntAdd(first, new SqlIntMult(first, second));


            
            

            Console.WriteLine(SqlCompiler.CompileExpr(add));
        }
    }
}
