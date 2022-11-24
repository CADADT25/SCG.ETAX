using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SCG.CAD.ETAX.API.Functions
{
    public class JwtAuthExternal
    {
        //public const string SecretKey = "JqPhHwU88vuxKsNWRZp5Fed2DkJZbSTe";
        public static VerifyUserDataFromJwt Authenticate(string jwtToken, out string errorMsg)
        {
            errorMsg = "";
            var ret = new VerifyUserDataFromJwt();
            var token = jwtToken;

            // checking request header value having required scheme "Bearer" or not.
            if (token == null || string.IsNullOrEmpty(token))
            {
                errorMsg = "JWT Token is Missing";
                return ret;
            }
            // Getting Token value from header values.
            var principal = AuthJwtToken(token).Result;

            if (principal == null)
            {
                errorMsg = "Invalid JWT Token";
            }
            else
            {
                ret = principal;
            }
            return ret;
        }
        private static bool ValidateToken(string token, out VerifyUserDataFromJwt ret)
        {
            ret = null;
            var simplePrinciple = GetPrincipal(token, out ret);
            if (simplePrinciple == null)
                return false;
            var identity = simplePrinciple.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            //var nameClaim = identity.FindFirst(ClaimTypes.Name);

            //if (string.IsNullOrEmpty(nameClaim?.Value))
            //    return false;

            return true;
        }
        protected static Task<VerifyUserDataFromJwt> AuthJwtToken(string token)
        {
            VerifyUserDataFromJwt ret;

            if (ValidateToken(token, out ret))
            {

                return Task.FromResult(ret);
            }

            return Task.FromResult<VerifyUserDataFromJwt>(null);
        }
        public static string GenerateJWTToken(string name, int expire_in_Minutes = 60)
        {
            var symmetric_Key = Convert.FromBase64String(new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("Jwt")["AutoLoginSecretKey"]);
            var token_Handler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var securitytokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, name)
                        }),

                Expires = now.AddMinutes(Convert.ToInt32(expire_in_Minutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetric_Key), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = token_Handler.CreateToken(securitytokenDescriptor);
            var token = token_Handler.WriteToken(stoken);

            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token, out VerifyUserDataFromJwt ret)
        {
            ret = null;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("Jwt")["AutoLoginSecretKey"]);
                var encodeSecretKey = System.Convert.ToBase64String(plainTextBytes);
                var symmetricKey = Convert.FromBase64String(encodeSecretKey);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                ret = ConvertClaimsToModel(jwtToken.Claims);

                return principal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private static VerifyUserDataFromJwt ConvertClaimsToModel(IEnumerable<Claim> claims)
        {
            VerifyUserDataFromJwt ret = null;
            Claim iat = claims.FirstOrDefault(t => t.Type.ToLower() == "iat");
            var externalId = claims.FirstOrDefault(t => t.Type.ToLower() == "externalid");
            var externalId2 = claims.FirstOrDefault(t => t.Type.ToLower() == "externalid2");
            var email = claims.FirstOrDefault(t => t.Type.ToLower() == "email");
            var userId = claims.FirstOrDefault(t => t.Type.ToLower() == "userid");
            var redirectUrl = claims.FirstOrDefault(t => t.Type.ToLower() == "redirecturl");
            if (iat != null)
            {
                if (ret == null) ret = new VerifyUserDataFromJwt();
                //ret.iat = DateTime.ParseExact(iat.Value.Replace("\"", ""), "yyyy-MM-ddTHH:mm:ss", null);
                var strDate = iat.Value.Replace("\"", "").Substring(0, 19);
                try
                {
                    ret.iat = DateTime.ParseExact(strDate, "yyyy-MM-dd" + "T" + "HH:mm:ss", null);
                }
                catch
                {
                    ret.iat = DateTime.ParseExact(strDate, "yyyy-MM-dd" + "T" + "HH:mm:ss", CultureInfo.InvariantCulture);
                }
                
            }
            if (externalId != null)
            {
                if (ret == null) ret = new VerifyUserDataFromJwt();
                ret.ExternalId = externalId.Value;
            }
            if (externalId2 != null)
            {
                if (ret == null) ret = new VerifyUserDataFromJwt();
                ret.ExternalId2 = externalId2.Value;
            }
            if (email != null)
            {
                if (ret == null) ret = new VerifyUserDataFromJwt();
                ret.Email = email.Value;
            }
            if (userId != null)
            {
                if (ret == null) ret = new VerifyUserDataFromJwt();
                ret.userId = userId.Value;
            }
            if (redirectUrl != null)
            {
                if (ret == null) ret = new VerifyUserDataFromJwt();
                ret.redirectUrl = redirectUrl.Value;
            }
            return ret;
        }
    }
}
