﻿using System;
using System.ComponentModel.DataAnnotations;

using Architect.Common.Enums;
using Architect.Common.Infrastructure.DataTransfer.Request;

namespace Architect.PersonFeature.DataTransfer.Request
{
    public class AddressRequest : RequestBase
    {
        public Country Country { get; set; }
        [Required]
        public string City { get; set; }
        [Range(1000,9999)]
        public int ZipCode { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string StreetNumber { get; set; }

        public virtual Database.Entities.Address CreateEntity()
        {
            var entity = new Database.Entities.Address()
            {
                Country = Country,
                City = City,
                ZipCode = ZipCode,
                Street = Street,
                StreetNumber = StreetNumber
            };

            return entity;
        }

        public virtual void UpdateEntity(Database.Entities.Address entity)
        {
            entity.ArgumentNullCheck(nameof(entity));

            Country = entity.Country;
            City = entity.City;
            ZipCode = entity.ZipCode;
            Street = entity.Street;
            StreetNumber = entity.StreetNumber;
        }
    }
}
