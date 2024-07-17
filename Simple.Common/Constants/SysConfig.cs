using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common.Constants
{
    public static class SysConfig
    {
        public const string SwaggerName = "Simple - API";

        public static List<string> CORS = new List<string>()
        {
            "*"
        };

        public const string CorsName = "nProx Origin";

        #region JWT

        public const string JWT_KEY = "s35436fgsđfsfsf33456f";
        public const int JWT_ACC_EXPIRE = 230;
        public const int JWT_REF_EXPIRE = 43200;

        #endregion JWT
    }
}
