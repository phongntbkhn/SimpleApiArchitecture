using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Simple.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common.Swagger
{
    public class SwaggerConfig
    {
        public static void AddConfig(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                        },
                        new List<string>()
                    }
                });
                var xmlFilename = "Simple.API.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

        }

        public static void AddUI(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                                                        description.GroupName.ToUpperInvariant());
                    }
                    options.DisplayRequestDuration();
                    options.DocumentTitle = SysConfig.SwaggerName;
                });
        }

        public static void AddReDoc(IApplicationBuilder app, IWebHostEnvironment env)
        {
            PhysicalFileProvider fileprovider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Contents/reDoc"));
            app.UseDefaultFiles(new DefaultFilesOptions
            {
                FileProvider = fileprovider,
                RequestPath = new PathString("/Documentation"),
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = fileprovider,
                RequestPath = new PathString("/Documentation"),
            });

            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = fileprovider,
                RequestPath = new PathString("/Documentation"),
                EnableDirectoryBrowsing = false
            });
        }
    }
}
