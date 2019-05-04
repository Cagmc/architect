using System;

using Architect.Database.Infrastructure;

namespace Architect.Database.Entities
{
    public class AddressAggregate : AggregateEntityBase
    {
        private AddressAggregate()
        {

        }

        public AddressAggregate(Address address, int rootId)
        {
            address.ArgumentNullCheck(nameof(address));
            rootId.ArgumentOutOfRangeCheck(nameof(rootId));

            AggregateRootId = rootId;
            Zip = address.ZipCode;
            Address = address.FullAddress;
            Country = Enum.GetName(typeof(Common.Enums.Country), address.Country);
        }

        public int Zip { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
    }
}
