using System.Collections.Generic;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Architect.WebApp.Filters
{
    public class SwaggerApiVersionHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "x-api-version",
                In = "header",
                Type = "string",
                Default = "1.0",
                Required = true // set to false if this is optional
            });
        }
    }
}
