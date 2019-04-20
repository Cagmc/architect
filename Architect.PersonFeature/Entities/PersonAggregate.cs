using System;

using Architect.Common.Enums;
using Architect.Database.Infrastructure;

namespace Architect.PersonFeature.Entities
{
    public class PersonAggregate : AggregateEntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public Color HairColor { get; set; }
        public Color EyeColor { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
