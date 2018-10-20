using greenlit.Services;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace greenlit.Helpers.Jwt
{
    public class Tokens
    {
        public static async Task<JwtResponse> GenerateJwt(ClaimsIdentity identity,
                                                     IJwtService jwtService,
                                                     string identifier,
                                                     JwtIssuerOptions jwtOptions)
        {
            return new JwtResponse
            {
                Id = identity.Claims.Single(c => c.Type == "id").Value,
                AuthToken = await jwtService.GenerateEncodedToken(identifier, identity),
                ExpiresIn = (int)jwtOptions.ValidFor.TotalSeconds,
                IssuedAt = jwtOptions.IssuedAt
            };
        }
    }
}
