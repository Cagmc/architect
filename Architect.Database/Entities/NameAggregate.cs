using System;
using System.Text;

using Architect.Database.Infrastructure;

namespace Architect.Database.Entities
{
    public class NameAggregate : AggregateEntityBase
    {
        private NameAggregate()
        {

        }

        public NameAggregate(Name name, int rootId)
        {
            name.ArgumentNullCheck(nameof(name));
            rootId.ArgumentOutOfRangeCheck(nameof(rootId));

            ShortName = $"{name.FirstName} {name.LastName}";
            LongName = CreateLongName(name);
            AggregateRootId = rootId;
        }

        public string ShortName { get; set; }
        public string LongName { get; set; }

        private string CreateLongName(Name name)
        {
            var builder = new StringBuilder();

            if (name.Title.IsNotNullOrEmpty())
            { 
                builder.Append($"{name.Title} {name.FirstName}");
            }
            else
            {
                builder.Append(name.FirstName);
            }

            if (name.MiddleName.IsNotNullOrEmpty())
            {
                builder.Append($" {name.MiddleName}");
            }

            builder.Append($" {name.LastName}");

            if (name.NickName.IsNotNullOrEmpty())
            {
                builder.Append($" as \"{name.NickName}\"");
            }

            return builder.ToString();
        }
    }
}
