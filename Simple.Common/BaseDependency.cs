using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Simple.Common.Constants;
using Simple.Common.Cors;
using Simple.Common.Exceptions;
using Simple.Common.Jwt;
using Simple.Common.Model;
using Simple.Common.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common
{
    public static class BaseDependency
    {
        public static void InjectDependencies(IServiceCollection services)
        {
            #region CORS
            CorsConfig cors = new CorsConfig();
            cors.Setup(services);
            #endregion CORS

            #region API Version
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            services.AddRouting(options => options.LowercaseUrls = true);
            #endregion API Version

            #region Swagger
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            SwaggerConfig.AddConfig(services);
            #endregion Swagger

            #region configure GZIP response
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();
            #endregion configure GZIP response

            #region JWT
            services.AddScoped<IJwtUtils, JwtUtils>();
            #endregion JWT
        }

        public static void Configure(ListServices services)
        {
            #region Swagger

            services.App.UseSwagger();
            SwaggerConfig.AddUI(services.App, services.Provider);

            #endregion Swagger

            #region Base

            services.App.UseResponseCompression();
            services.App.UseHttpsRedirection();
            services.App.UseRouting();

            services.App.UseCors(SysConfig.CorsName);
            services.App.UseAuthentication();
            services.App.UseAuthorization();

            #endregion Base

            #region Middleware

            services.App.UseMiddleware<HandlerMiddlewareException>();
            services.App.UseMiddleware<JwtException>();

            #endregion Middleware
        }
    }
}
