using Data;
using Microsoft.EntityFrameworkCore;
using Server.Mappers;

namespace Server;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ApplicationDbContext>();
        
        builder.Services.AddAutoMapper(typeof(AppMappingProfile));
        
        var app = builder.Build();
        
        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}