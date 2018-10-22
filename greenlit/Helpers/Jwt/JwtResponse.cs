using System;

namespace greenlit.Helpers.Jwt
{
    public class JwtResponse
    {
        public string Id { get; set; }
        public string AuthToken { get; set; }
        public int ExpiresIn { get; set; }
        public DateTime IssuedAt { get; set; }
    }
}
