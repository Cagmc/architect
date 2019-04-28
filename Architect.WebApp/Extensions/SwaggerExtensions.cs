using Architect.WebApp.Filters;

using Microsoft.AspNetCore.Builder;

using Swashbuckle.AspNetCore.Swagger;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Architect API",
                    Version = "v1",
                    Description = "Sample experimental Web API",
                    License = new License
                    {
                        Name = "MIT License",
                        Url = "https://github.com/Cagmc/architect/blob/master/LICENSE"
                    },
                    Contact = new Contact
                    {
                        Name = "István Rózsa",
                        Url = "https://github.com/Cagmc"
                    }
                });
                c.OperationFilter<SwaggerApiVersionHeaderFilter>();
            });

            return services;
        }

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Architect API V1");
                c.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}
