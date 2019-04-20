using System;

using Architect.Common.Enums;
using Architect.Common.Infrastructure.DataTransfer.Response;

namespace Architect.PersonFeature.DataTransfer.Response
{
    public class PersonViewModel : ViewModelBase
    {
        public PersonViewModel(int id, Database.Entities.Person entity) : base(id)
        {
            entity.ArgumentNullCheck(nameof(entity));

            Height = entity.Height;
            Weight = entity.Weight;
            HairColor = entity.HairColor;
            EyeColor = entity.EyeColor;
            BirthDate = entity.BirthDate;

            Name = new NameViewModel(entity.NameId, entity.Name);

            if (entity.AddressId.HasValue)
            {
                Address = new AddressViewModel(entity.AddressId.Value, entity.Address);
            }
        }

        public NameViewModel Name { get; set; }
        public AddressViewModel Address { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public Color HairColor { get; set; }
        public Color EyeColor { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
