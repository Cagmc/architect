using System;

using Architect.Common.Enums;
using Architect.Common.Infrastructure.DataTransfer.Response;

namespace Architect.PersonFeature.DataTransfer.Response
{
    public class AddressViewModel : ViewModelBase
    {
        public AddressViewModel(int id, Database.Entities.Address entity) : base(id)
        {
            entity.ArgumentNullCheck(nameof(entity));

            Country = entity.Country;
            City = entity.City;
            ZipCode = entity.ZipCode;
            Street = entity.Street;
            StreetNumber = entity.StreetNumber;
        }

        public Country Country { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
    }
}
