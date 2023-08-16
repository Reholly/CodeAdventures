using Blazored.LocalStorage;
using Client.ControllerClients;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.DTO;
using Shared.Requests.Articles;
using Shared.Requests.Auth;
using Shared.Responses.Auth;

namespace Client;

public class FacadeApi
{
    private readonly IArticlesControllerClient _articlesControllerClient;
    private readonly IAuthControllerClient _authControllerClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public FacadeApi(
        IArticlesControllerClient articlesControllerClient, 
        IAuthControllerClient authControllerClient, 
        ILocalStorageService localStorage, 
        AuthenticationStateProvider authenticationStateProvider
        )
    {
        _articlesControllerClient = articlesControllerClient;
        _authControllerClient = authControllerClient;
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<List<ArticleModel>> GetArticles(GetArticlesRequest request)
    {
        try
        {
            var response = await _articlesControllerClient.GetArticles(request);
            if (response.Articles.Count == 0)
            {
                throw new Exception();
            }

            return response.Articles.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
    {
        try
        {
            var response = await _authControllerClient.RegisterUser(request);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<LogInResponse> LogInUser(LogInRequest request)
    {
        try
        {
            var response = await _authControllerClient.LogIn(request);
            if (!response.IsSucceeded)
            {
                return response;
            }

            await _localStorage.SetItemAsync("token", response.Token.Token);
            await _localStorage.SetItemAsync("expiry", response.Token.ExpireTime);
            await _authenticationStateProvider.GetAuthenticationStateAsync();

            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task LogOutUser(LogOutRequest request)
    {
        try
        {
            await _authControllerClient.LogOut(request);
            
            await _localStorage.RemoveItemAsync("token");
            await _localStorage.RemoveItemAsync("expiry");
            await _authenticationStateProvider.GetAuthenticationStateAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}