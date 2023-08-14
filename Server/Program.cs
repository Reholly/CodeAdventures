namespace Server;

internal class Program
{
    public static void Main(string[] args)
    {
        Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>()
                    .UseConfiguration(new ConfigurationBuilder()
                        .AddJsonFile("config.json")
                        .Build());
            })
            .Build()
            .Run();
    }
}