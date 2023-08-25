using System.Security.Claims;
using System.Text.Json;
using Server.Exceptions;

namespace Server.Services;

public class TokenParseService
{
    public ClaimsPrincipal ParseClaimsPrincipalFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        if (keyValuePairs != null)
        {
            var identity = new ClaimsIdentity(keyValuePairs
                .Select(kvp => new Claim(kvp.Key, kvp.Value.ToString() 
                                                  ?? string.Empty)));
            return new ClaimsPrincipal(identity);
        }

        throw new InternalServiceException();
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}