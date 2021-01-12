using System;
using SqlDsl.Core;

namespace SqlDsl.ConsoleApp
{
    record Foo(string Name);
    
    class Program
    {
        public record Customer(string TableName, SqlIntColumn Id, SqlIntColumn Age) 
            : SqlTable<SqlIntColumn, SqlIntColumn>(TableName, Id, Age);

        static Customer CustomerTable => new Customer("Customers", new SqlIntColumn("Id"), new SqlIntColumn("Age"));

        static void Main(string[] args)
        {

            var query = CustomerTable.From().Select(x => (Id: x.Id, Age: x.Age + 1));
            // SELECT x.Id, x.Age + 1 FROM Customers x

            Console.WriteLine(query.CompileSqlQuery());
            
        }
    }
}
