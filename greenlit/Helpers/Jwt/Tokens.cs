using greenlit.Services;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace greenlit.Helpers.Jwt
{
    public class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity,
                                                     IJwtService jwtService,
                                                     string identifier,
                                                     JwtIssuerOptions jwtOptions,
                                                     JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await jwtService.GenerateEncodedToken(identifier, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}
