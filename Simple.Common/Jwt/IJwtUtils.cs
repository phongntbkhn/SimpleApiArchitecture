using Simple.Common.Model;
using Simple.Common.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common.Jwt
{
    public interface IJwtUtils
    {
        string GenerateJwtToken(LoginReq user, string Id, string secretKey = "");

        string GenerateRefreshToken(LoginReq user, out DateTime Expire);

        JwtRes? ValidateJwtToken(string token);

        bool ValidateJwtPrincipalSecretKey(string token);

        RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
