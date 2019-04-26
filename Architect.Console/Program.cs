using System;
using System.Linq;
using System.Threading.Tasks;
using Architect.PersonFeature.Events;
using Microsoft.EntityFrameworkCore;

namespace Architect.Architect.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
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
