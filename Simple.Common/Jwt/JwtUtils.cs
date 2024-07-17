using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Simple.Common.Constants;
using Simple.Common.Helper;
using Simple.Common.Model;
using Simple.Common.Model.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common.Jwt
{
    public class JwtUtils : IJwtUtils
    {
        private readonly ILogger<JwtUtils> _logger;

        public JwtUtils(ILogger<JwtUtils> logger)
        {
            _logger = logger;
        }

        public string GenerateJwtToken(LoginReq user, string Id, string secretKey = "")
        {
            // generate token that is valid for 15 minutes
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SysConfig.JWT_KEY);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("username", user.UserName.ToString()), new Claim("UserID", Id), new Claim("SecretKey", secretKey) }),
                Expires = DateTime.UtcNow.AddMinutes(SysConfig.JWT_ACC_EXPIRE),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken(LoginReq user, out DateTime Expire)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SysConfig.JWT_KEY);
            Expire = DateTime.UtcNow.AddMinutes(SysConfig.JWT_REF_EXPIRE);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("username", user.UserName.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(SysConfig.JWT_REF_EXPIRE),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.UtcNow.AddMilliseconds(DateTime.Now.Millisecond)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            // generate token that is valid for 7 days
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                ExpiredDate = DateTime.UtcNow.AddDays(1),
                CreatedDate = DateTime.UtcNow,
                IP = ipAddress
            };

            return refreshToken;
        }

        public bool ValidateJwtPrincipalSecretKey(string token)
        {
            if (token == null)
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SysConfig.JWT_KEY);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var secretKey = jwtToken.Claims.FirstOrDefault(x => x.Type == "SecretKey")?.Value?.ToString();

                if (secretKey == null || secretKey.IsNullOrWriteSpace())
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public JwtRes? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SysConfig.JWT_KEY);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value?.ToString();
                var phonenumber = jwtToken.Claims.FirstOrDefault(x => x.Type == "phonenumber")?.Value?.ToString();
                var secretKey = jwtToken.Claims.FirstOrDefault(x => x.Type == "SecretKey")?.Value?.ToString();
                var userName = jwtToken.Claims.FirstOrDefault(x => x.Type == "username")?.Value?.ToString();
                var emp = jwtToken.Claims.FirstOrDefault(x => x.Type == "EmpId")?.Value?.ToString();

                JwtRes response = new JwtRes()
                {
                    UserId = userId,
                    SecretKey = secretKey,
                    UserName = userName,
                    ErrorCode = null
                };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
