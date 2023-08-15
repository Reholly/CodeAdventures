using Blazored.LocalStorage;

namespace Client.HttpHandlers;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly ILocalStorageService _authTokenStore;

    public AuthHeaderHandler(ILocalStorageService authTokenStore)
    {
        _authTokenStore = authTokenStore ?? throw new ArgumentNullException(nameof(authTokenStore));
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _authTokenStore.GetItemAsStringAsync("token", cancellationToken);
        
        if (token is not null)
        {
            var readyToken = token.Replace("\"", "");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", "Bearer " + readyToken);
        }

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}