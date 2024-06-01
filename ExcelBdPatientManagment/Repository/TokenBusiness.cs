using API.Repository.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Repository
{

    public class TokenBusiness : IToken
    {
        private readonly IConfiguration config;
        public TokenBusiness(IConfiguration config)
        {
            this.config = config;
        }

        // Generate JWT
        public string GenerateToken(string Id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.Name, Id),
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        // Validation user ID from token
        public static bool Validation(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("tNdsffdsfdsffsXWsfsdffEq9NNgsdfdsfNETnnLdfsdssdffVdsfsIuUyRfdsfsqu");

            // Token validation parameters
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            try
            {
                // Extract user ID from token
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(
                    token,
                    tokenValidationParameters,
                    out SecurityToken validatedToken
                    );

                return true;
            }
            catch (SecurityTokenException ex)
            {
                return false;
            }

        }

        // Retrieve user ID from token
        public string GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]!);

            // Token validation parameters
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            try
            {
                // Extract user ID from token
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(
                    token,
                    tokenValidationParameters,
                    out SecurityToken validatedToken
                    );

                Claim claim = claimsPrincipal.FindFirst(ClaimTypes.Name);

                return claim.Value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
