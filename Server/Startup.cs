using System.Text;
using Data;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Mapping;
using Server.Services.ArticleServices;
using Server.Services.AuthServices;
using Server.Services.UserServices;

namespace Server;

public class Startup
{
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _env;

    public Startup(IConfiguration config, IWebHostEnvironment env)
    {
        _config = config;
        _env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var connStr = _config.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Conn String not found");
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connStr));
        
        services.AddScoped(typeof(IRepository<>), typeof(DefaultRepository<>));
        
        services.AddAutoMapper(typeof(AppMappingProfile));
        services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {//tut mb problems
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = _config.GetValue<string>("Audience"),
                    ValidIssuer =  _config.GetValue<string>("Issuer"),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(5),
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("SecurityKey")!)),
                    ValidateIssuerSigningKey = true
                };
            });
        services.AddAuthorization();
        services.AddControllers();
        services.AddMediatR(mediatr =>
        {
            mediatr.RegisterServicesFromAssemblyContaining<Program>();
        });
        services.AddCors(policy =>
        {
            policy.AddPolicy("OpenCorsPolicy", opt =>
            {
                opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, IdentityAuthService>();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseCors("OpenCorsPolicy");
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(e => e.MapControllers());
    }
}