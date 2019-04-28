﻿using Architect.Common.Infrastructure;
using Architect.Database;
using Architect.PersonFeature;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Architect.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationDatabase")));
            services.AddDbContext<PersonDatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PersonFeatureDatabase")));

            services.AddPeopleFeature();
            services.AddSingleton<IEventDispatcher, EventDispatcher>();

            services.AddApiVersioning(o => {
                o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });

            services.ConfigureSwagger();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            DatabaseContext context, PersonDatabaseContext pContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            context.Database.Migrate();
            pContext.Database.Migrate();

            app.ConfigureSwagger();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}