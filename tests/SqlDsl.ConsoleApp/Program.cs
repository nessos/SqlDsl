using System;
using SqlDsl.Core;

namespace SqlDsl.ConsoleApp
{
    record Foo(string Name);
    
    class Program
    {


        static void Main(string[] args)
        {


            var customers = (Table: "Customer", (Id: new SqlIntColumn("Id"), 
                                                 Age: new SqlIntColumn("Age")));
            var query = customers.From().Select(x => (Id: x.Id, Age: x.Age + 1));
            // SELECT x.Id, x.Age + 1 FROM Customers x

            Console.WriteLine(query.CompileSqlQuery());
            
        }
    }
}
