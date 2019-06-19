using System;

using Microsoft.EntityFrameworkCore;

namespace Architect.Common.Infrastructure
{
    public abstract class ServiceBase<TDbContext>  : IService
        where TDbContext : DbContext
    {
        protected readonly TDbContext context;

        public ServiceBase(TDbContext context)
        {
            this.context = context.ArgumentNullCheck(nameof(context));
        }
    }
}
