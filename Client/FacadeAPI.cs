using Blazored.LocalStorage;
using Client.ControllerClients;
using Microsoft.AspNetCore.Components.Authorization;
using Refit;
using Shared.DTO;
using Shared.Requests.Articles;
using Shared.Requests.Auth;
using Shared.Responses.Articles;
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
        AuthenticationStateProvider authenticationStateProvider)
    {
        _articlesControllerClient = articlesControllerClient;
        _authControllerClient = authControllerClient;
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<List<ArticleModel>> GetArticles(GetArticlesRequest request)
    {
        var response = await _articlesControllerClient.GetArticles(request);
        return response.Content!.Articles.ToList();
    }

    public async Task<ArticleModel?> GetArticle(GetArticleRequest request)
    {
        try
        {
            var response = await _articlesControllerClient.GetArticle(request);
            return response.Content?.Article;
        }
        catch (ApiException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<CreateArticleResponse?> CreateArticle(CreateArticleRequest request)
    {
        try
        {
            var response = await _articlesControllerClient.CreateArticle(request);
            return response.Content;
        }
        catch (ApiException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<EditArticleResponse?> EditArticle(EditArticleRequest request)
    {
        try
        {
            var response = await _articlesControllerClient.EditArticle(request);
            return response.Content;
        }
        catch (ApiException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<DeleteArticleResponse> DeleteArticle(DeleteArticleRequest request)
    {
        try
        {
            var response = await _articlesControllerClient.DeleteArticle(request);
            return response.Content!;
        }
        catch (ApiException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> CreateUser(CreateUserRequest request)
    {
        var response = await _authControllerClient.RegisterUser(request);
        if (!response.IsSuccessStatusCode)
        {
            throw response.Error;
        }

        return true;
    }


    public async Task<LogInResponse?> LogInUser(LogInRequest request)
    {
        var response = await _authControllerClient.LogIn(request);
        if (!response.IsSuccessStatusCode)
        {
            throw response.Error;
        }

        await _localStorage.SetItemAsync("token", response.Content.Token.Token);
        await _localStorage.SetItemAsync("expiry", response.Content.Token.ExpireTime);
        await _authenticationStateProvider.GetAuthenticationStateAsync();
            
        return response.Content;
    }

    public async Task<bool> LogOutUser(LogOutRequest request)
    {
        var response = await _authControllerClient.LogOut(request);
        if (!response.IsSuccessStatusCode)
        {
            throw response.Error;
        }

        await _localStorage.RemoveItemAsync("token");
        await _localStorage.RemoveItemAsync("expiry");
        await _authenticationStateProvider.GetAuthenticationStateAsync();

        return true;
    }
}