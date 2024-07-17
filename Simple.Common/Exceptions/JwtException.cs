using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common.Exceptions
{
    public class JwtException : Exception
    {
        public JwtException() : base()
        {
        }
        public JwtException(string message) : base(message)
        {
        }

        public JwtException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
