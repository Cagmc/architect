using System;
using System.ComponentModel.DataAnnotations;

namespace Architect.PersonFeature.DataTransfer.Request
{
    public class NameRequest
    {
        public string Title { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string NickName { get; set; }

        public virtual Database.Entities.Name CreateEntity()
        {
            var entity = new Database.Entities.Name()
            {
                FirstName = FirstName,
                LastName = LastName,
                Title = Title,
                MiddleName = MiddleName,
                NickName = NickName
            };

            return entity;
        }

        public virtual void UpdateEntity(Database.Entities.Name entity)
        {
            entity.ArgumentNullCheck(nameof(entity));

            entity.FirstName = FirstName;
            entity.LastName = LastName;
            entity.MiddleName = MiddleName;
            entity.Title = Title;
            entity.NickName = NickName;
        }
    }
}
