using System;
using System.Collections.Generic;
using System.Text;
using Architect.Common.Infrastructure.DataTransfer.Request;

namespace Architect.PersonFeature.DataTransfer.Request
{
    public class UpdatePersonRequest : UpdateRequestBase
    {
        public UpdatePersonRequest(int id) : base(id)
        {
        }
    }
}
