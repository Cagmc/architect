using System;

using Architect.Common.Enums;
using Architect.Common.Infrastructure;

namespace Architect.Database.Entities
{
    public class BackgroundJob : EntityBase
    {
        public string Data { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public JobType Type { get; set; }
        public JobStatus Status { get; set; }
    }
}
