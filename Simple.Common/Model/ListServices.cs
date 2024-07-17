using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common.Model
{
    public class ListServices
    {
        public IApplicationBuilder App { get; set; }
        public IWebHostEnvironment Env { get; set; }
        public IApiVersionDescriptionProvider Provider { get; set; }
        public ILoggerFactory logFactory { get; set; }
    }
}
