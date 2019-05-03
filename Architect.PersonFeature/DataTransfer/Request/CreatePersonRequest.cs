using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Architect.Common.Enums;
using Architect.Common.Infrastructure.DataTransfer.Request;

namespace Architect.PersonFeature.DataTransfer.Request
{
    public class CreatePersonRequest : CreateRequestBase
    {
        [Required]
        public NameRequest Name { get; set; }
        [Range(50, 235)]
        public int Height { get; set; }
        [Range(3, 200)]
        public int Weight { get; set; }
        public Color HairColor { get; set; }
        public Color EyeColor { get; set; }
        public DateTime BirthDate { get; set; }

        public AddressRequest Address { get; set; }

        public virtual Database.Entities.Person CreateEntity()
        {
            var name = Name.CreateEntity();
            var address = Address?.CreateEntity();

            var entity = new Database.Entities.Person()
            {
                Name = name,
                Address = address,
                Height = Height,
                Weight = Weight,
                HairColor = HairColor,
                EyeColor = EyeColor,
                BirthDate = BirthDate
            };

            return entity;
        }

        public override IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            var results = base.Validate(validationContext) as IList<ValidationResult>;

            if (BirthDate > DateTime.UtcNow)
            {
                results.Add(new ValidationResult($"{BirthDate} cannot be later then now", new string[] { nameof(BirthDate) }));
            }

            return results;
        }
    }
}
