using System;

using Architect.Common.Infrastructure.DataTransfer.Response;

namespace Architect.PersonFeature.DataTransfer.Response
{
    public class NameViewModel : ViewModelBase
    {
        public NameViewModel(int id, Database.Entities.Name entity) : base(id)
        {
            entity.ArgumentNullCheck(nameof(entity));

            Title = entity.Title;
            FirstName = entity.FirstName;
            MiddleName = entity.MiddleName;
            LastName = entity.LastName;
            NickName = entity.NickName;
        }

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
    }
}
