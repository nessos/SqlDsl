using System;
using SqlDsl.Core;

namespace SqlDsl.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var add = new SqlIntAdd(new SqlIntValue(1), new SqlIntValue(1));

            Console.WriteLine("Hello World!");
        }
    }
}
