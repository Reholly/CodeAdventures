using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Server.Services.UserServices;
using Server.Services.AuthServices.AuthResponses;

namespace Server.Services.AuthServices;

public class IdentityAuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IUserService _userService;
    private readonly IConfiguration _config;

    public IdentityAuthService(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IUserService userService,
        IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userService = userService;
        _config = config;
    }
    
    public async Task<LogInResponse> LogInUserAsync(string email, string password)
    {
        var identityUser = await _userManager.FindByEmailAsync(email);
        
        if (identityUser == null)
            return new LogInResponse
            {
                IsSucceeded = false,
                Errors = new []{ $"Пользователь с такой почтой не существует: {email}"}
            };
        
        var result = await _userManager.CheckPasswordAsync(identityUser, password);

        if (!result)
            return new LogInResponse
            {
                IsSucceeded = false,
                Errors = new []{ $"Неверный пароль."}
            };
        
        var claims = new[]
        {
            new Claim("Email", email),
            new Claim("Name", email),
            new Claim(ClaimTypes.Role, "Student")
        };
        
        var token = CreateJwtToken(claims);
        var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

        return new LogInResponse
        {
            IsSucceeded = true,
            Token = tokenAsString,
            TokenExpireDate = token.ValidTo
        };
    }

    private JwtSecurityToken CreateJwtToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("SecurityKey")));
        var token = new JwtSecurityToken(
            issuer: _config.GetValue<string>("Issuer"),
            audience: _config.GetValue<string>("Audience"),
            claims: claims,
            expires: DateTime.Now.AddHours(_config.GetValue<int>("TokenExpiresTime")),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        
        return token;
    }

    public async Task<IdentityResult> RegisterUserAsync(User user, string password)
    {
        var identityUser = new IdentityUser
        {
            UserName = user.Username,
            Email = user.Email
        };
        var userFromDb = await _userService.FindByEmail(user.Email);
        if (userFromDb is null)
        {
            await _userService.CreateUserAsync(user);
            return await _userManager.CreateAsync(identityUser, password);
        }
        
        throw new InvalidOperationException("Пользователь уже существует");
    }

    public async Task LogOutAsync() => await _signInManager.SignOutAsync();

    public async Task<ClaimsPrincipal> GetCurrentUserFromToken(string token)
    {
        var identity = new ClaimsIdentity();

        if (!string.IsNullOrEmpty(token))
        {
            identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        }

        var user = new ClaimsPrincipal(identity);

        return user;
    }
    
    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
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