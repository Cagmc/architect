using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Architect.Common.Infrastructure
{
    public class EventDispatcher : IEventDispatcher
    {
        private int id = 0;
        public virtual void Dispatch(IEvent domainEvent)
        {
            var cid = id++;
            Task.Run(async () => 
            {
                for (int i = 0; i < 12; i++)
                {
                    System.Diagnostics.Debug.WriteLine($"Dispatch: {cid}");
                    Console.WriteLine($"Dispatch: {cid}");
                    await Task.Delay(5000); 
                }
            });
        }
    }
}
