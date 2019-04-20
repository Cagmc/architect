using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Architect.Architect.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var options = new DbContextOptionsBuilder()
                .UseSqlServer("Server=CPTCAGMCPC\\SQLEXPRESS;Database=architect;Integrated security=True;").Options;

            using (var context = new Database.DatabaseContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }
        }
    }
}
