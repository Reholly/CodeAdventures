using Blazored.LocalStorage;
using Client.ClientSideAuthProviders;
using Client.ControllerClients;
using Client.HttpHandlers;
using Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

namespace Client;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");
        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        builder.Services.AddBlazoredLocalStorage();
        
        builder.Services.AddTransient<AuthHeaderHandler>();

        builder.Services.AddRefitClient<IArticlesControllerClient>()
            .ConfigureHttpClient(config 
                => config.BaseAddress = new Uri("http://localhost:5239/api"))
            .AddHttpMessageHandler<AuthHeaderHandler>();
        
        builder.Services.AddTransient<FacadeApi>();
        
        builder.Services.AddRefitClient<IAuthControllerClient>()
            .ConfigureHttpClient(config 
                => config.BaseAddress = new Uri("http://localhost:5239/api"))
            .AddHttpMessageHandler<AuthHeaderHandler>();
        
        builder.Services.AddOptions();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<ToastService>();
        
        await builder.Build().RunAsync();
    }
}