using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Architect.Common.Infrastructure.DataTransfer.Request
{
    public abstract class RequestBase : IValidatableObject
    {
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }

        protected virtual IList<ValidationResult> ValidateProperties<T>()
        {
            var results = new List<ValidationResult>();

            foreach (var property in typeof(T).GetProperties())
            {
                ValidateProperty(results, property.GetValue(this), property.Name);
            }

            return results;
        }

        private bool ValidateProperty(IList<ValidationResult> results, object value, string name)
        {
            var isSuccess = Validator.TryValidateProperty(value,
                new ValidationContext(this, null, null) { MemberName = name },
                results);

            return isSuccess;
        }
    }
}
