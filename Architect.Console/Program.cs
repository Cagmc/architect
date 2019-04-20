using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Architect.Architect.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //var options = new DbContextOptionsBuilder()
            //    .UseSqlServer("Server=CPTCAGMCPC\\SQLEXPRESS;Database=architect;Integrated security=True;").Options;

            //using (var context = new Database.DatabaseContext(options))
            //{
            //    context.Database.EnsureDeleted();
            //    context.Database.Migrate();
            //}

            var eventDispatcher = new Common.Infrastructure.EventDispatcher();
            eventDispatcher.Dispatch(null);
            eventDispatcher.Dispatch(null);
            eventDispatcher.Dispatch(null);
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Main");
                System.Diagnostics.Debug.WriteLine("Main");
                Task.Delay(1000).GetAwaiter().GetResult();
            }
        }
    }
}
