using System.ComponentModel.DataAnnotations;

using Architect.Common.Infrastructure.DataTransfer.Request;

namespace Architect.UserFeature.DataTransfer.Request
{
    public class RegistrationRequest : RequestBase
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
