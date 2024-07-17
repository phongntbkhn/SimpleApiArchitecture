using Microsoft.Extensions.DependencyInjection;
using Simple.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common.Cors
{
    public class CorsConfig
    {
        public void Setup(IServiceCollection services)
        {
            string origins = "";
            if (SysConfig.CORS == null || SysConfig.CORS.Count <= 0)
                origins = "*";
            else
                SysConfig.CORS.ForEach(x => { origins = origins + x.Trim(); });

            if (origins.Contains("*"))
            {
                services.AddCors(x =>
                {
                    x.AddPolicy(SysConfig.CorsName, builder =>
                    {
                        builder.AllowAnyMethod()
                               .AllowAnyHeader()
                               .SetIsOriginAllowed(origin => true)
                               .AllowCredentials()
                               .WithExposedHeaders("Content-Disposition");
                    });
                });
            }
            else
            {
                services.AddCors(x =>
                {
                    x.AddPolicy(SysConfig.CorsName, builder =>
                    {
                        builder.AllowAnyMethod()
                               .AllowAnyHeader()
                               .WithOrigins(origins)
                               .AllowCredentials()
                               .WithExposedHeaders("Content-Disposition");
                    });
                });
            }
        }
    }
}
