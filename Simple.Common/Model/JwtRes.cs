using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common.Model
{
    public class JwtRes
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? SecretKey { get; set; }
        public string? ErrorCode { get; set; }
    }
}
