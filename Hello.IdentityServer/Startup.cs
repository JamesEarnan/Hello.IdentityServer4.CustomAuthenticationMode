using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hello.IdentityServer.Services;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuickstartIdentityServer;

namespace Hello.IdentityServer
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var builder = services.AddIdentityServer()
             .AddDeveloperSigningCredential()
             .AddInMemoryPersistedGrants()
             .AddInMemoryIdentityResources(Config.GetIdentityResources())
             .AddInMemoryApiResources(Config.GetApiResources())
             .AddInMemoryClients(Config.GetClients());

            builder.Services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            builder.Services.AddTransient<IProfileService, ProfileService>();

            services.AddAuthentication(
               IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = "http://localhost:60820"; // Auth Server
                        options.RequireHttpsMetadata = false; // only for development
                        options.ApiName = "api1"; // API Resource Id
                    });

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
