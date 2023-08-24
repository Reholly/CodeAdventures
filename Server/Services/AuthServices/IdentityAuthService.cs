using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data.Entities;
using Data.Repositories;
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
    private readonly IRepository<IdentityUserClaim<string>> _claimsDb;

    public IdentityAuthService(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IUserService userService,
        IConfiguration config, 
        IRepository<IdentityUserClaim<string>> claimsDb)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userService = userService;
        _config = config;
        _claimsDb = claimsDb;
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

        var identityClaims = (await _claimsDb.GetTableAsync())
            .Where(x => x.UserId == identityUser.Id)
            .Select(x => x.ToClaim());
        
        var claims = new List<Claim>
        {
            new Claim("Email", email),
            new Claim("Name", email),
        };
        
        foreach (var claim in identityClaims)
        {
            claims.Add(claim);
        }
        
        var token = CreateJwtToken(claims.ToArray());
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
            var result = await _userManager.CreateAsync(identityUser, password);
            if (result.Succeeded)
            {
                await _claimsDb.AddAsync(new IdentityUserClaim<string>
                {
                    ClaimType = ClaimTypes.Role,
                    ClaimValue = "Student",
                    UserId = identityUser.Id
                });
            }
        }
        
        throw new InvalidOperationException("Пользователь уже существует");
    }

    public async Task LogOutAsync() => await _signInManager.SignOutAsync();
}