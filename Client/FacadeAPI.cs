using Blazored.LocalStorage;
using Client.ControllerClients;
using Microsoft.AspNetCore.Components.Authorization;
using Refit;
using Shared.DTO;
using Shared.Requests.Articles;
using Shared.Requests.Auth;
using Shared.Requests.Index;
using Shared.Responses.Articles;
using Shared.Responses.Auth;
using Shared.Responses.Index;

namespace Client;

public class FacadeApi
{
    private readonly IArticlesControllerClient _articlesControllerClient;
    private readonly IAuthControllerClient _authControllerClient;
    //private readonly IIndexControllerClient _indexControllerClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly List<TidingModel> _tidingModels = new();

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

    public async Task<ArticleModel?> EditArticle(EditArticleRequest request)
    {
        var response = await _articlesControllerClient.EditArticle(request);
        if (!response.IsSuccessStatusCode)
        {
            throw response.Error;
        }

        return response.Content.EditedArticle;
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

    public async Task<TidingModel> CreateTiding(CreateArticleRequest request)
    {
        var createdTiding = new TidingModel
        {
            Title = request.Title,
            Text = request.Text,
            PublicationDate = DateTime.Now
        };
        _tidingModels.Add(createdTiding);
        await Task.Delay(TimeSpan.FromSeconds(3));
        return createdTiding;
    }

    public async Task<bool> DeleteTiding(DeleteTidingRequest request)
    {
        var tiding = _tidingModels.Find(t => t.PublicationDate == request.PublicationDate);
        await Task.Delay(TimeSpan.FromSeconds(3));
        if (tiding is null)
        {
            throw new Exception("Not found (404).");
        }

        _tidingModels.Remove(tiding);
        return true;
    }

    public async Task<List<TidingModel>> GetAllTidings(GetAllTidingsRequest request)
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        return _tidingModels;
    }

    public async Task<TidingModel> GetLastTiding(GetLastTidingRequest request)
    {
        await Task.Delay(TimeSpan.FromSeconds(4));
        return _tidingModels.Last();
    }

    public async Task<bool> UpdateTiding(UpdateTidingRequest request)
    {
        var tiding = _tidingModels.Find(t => t.PublicationDate == request.PublicationDate);
        await Task.Delay(TimeSpan.FromSeconds(4));
        if (tiding is null)
        {
            throw new Exception("Not found (404).");
        }
        
        var index = _tidingModels.IndexOf(tiding);
        tiding.Text = request.Text;
        tiding.Title = request.Title;
        _tidingModels[index] = tiding;
        await Task.Delay(TimeSpan.FromSeconds(3));
        
        return true;
    }
}