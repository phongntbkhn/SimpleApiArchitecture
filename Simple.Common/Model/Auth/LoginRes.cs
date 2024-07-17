using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common.Model.Auth
{
    public class LoginRes
    {
        public string Token { get; set; }
        public DateTime Expiration {  get; set; }
    }
}
