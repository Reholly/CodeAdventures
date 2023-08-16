using Client.ControllerClients;
using Shared.DTO;
using Shared.Requests.Articles;
using Shared.Requests.Auth;
using Shared.Responses.Auth;

namespace Client;

public class FacadeApi
{
    private readonly IArticlesControllerClient _articlesControllerClient;
    private readonly IAuthControllerClient _authControllerClient;

    public FacadeApi(
        IArticlesControllerClient articlesControllerClient, 
        IAuthControllerClient authControllerClient
        )
    {
        _articlesControllerClient = articlesControllerClient;
        _authControllerClient = authControllerClient;
    }

    public async Task<ICollection<ArticleModel>> GetArticles(GetArticlesRequest request)
    {
        try
        {
            var response = await _articlesControllerClient.GetArticles(request);
            if (response.Articles.Count == 0)
            {
                throw new Exception();
            }

            return response.Articles;
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
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}