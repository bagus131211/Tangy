using System.Security.Claims;
using System.Text.Json;

namespace TangyWeb.Client.Helper
{
    public static class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var bytes = ParseBase64WithoutPadding(payload);
            var kvp = JsonSerializer.Deserialize<Dictionary<string, object>>(bytes);

            claims.AddRange(kvp.Select(k => new Claim(k.Key, k.Value.ToString())));
            return claims;
        }

        static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
