using System;
using System.ComponentModel.DataAnnotations;

using Architect.Common.Enums;
using Architect.Common.Infrastructure.DataTransfer.Request;

namespace Architect.PersonFeature.DataTransfer.Request
{
    public class UpdatePersonRequest : UpdateRequestBase
    {
        public UpdatePersonRequest(int id) : base(id)
        {
        }

        [Required]
        public NameRequest Name { get; set; }

        public int Height { get; set; }
        public int Weight { get; set; }
        public Color HairColor { get; set; }
        public Color EyeColor { get; set; }
        public DateTime BirthDate { get; set; }

        public AddressRequest Address { get; set; }

        public virtual void UpdateEntity(Database.Entities.Person entity)
        {
            entity.ArgumentNullCheck(nameof(entity));

            entity.Height = Height;
            entity.Weight = Weight;
            entity.HairColor = HairColor;
            entity.EyeColor = EyeColor;
            entity.BirthDate = BirthDate;

            Name.UpdateEntity(entity.Name);

            if (Address == null)
            {
                entity.Address = null;
            }
            else
            {
                Address?.UpdateEntity(entity.Address);
            }
        }
    }
}
