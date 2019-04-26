using System;

using Architect.Common.Enums;
using Architect.Database.Infrastructure;

namespace Architect.PersonFeature.Entities
{
    public class PersonAggregate : AggregateEntityBase
    {
        public PersonAggregate(Database.Entities.Person person)
        {
            person.ArgumentNullCheck(nameof(person));

            AggregateRootId = person.Id;
            CreatedDate = DateTime.UtcNow;
            Name = person.Name.FullName;
            Address = person.Address.FullAddress;
            Height = person.Height;
            Weight = person.Weight;
            HairColor = person.HairColor;
            EyeColor = person.EyeColor;
            BirthDate = person.BirthDate;
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public Color HairColor { get; set; }
        public Color EyeColor { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
