using MediatR;
using Server.Services.TidingServices;
using Shared.Requests.Index;
using Shared.Responses.Index;

namespace Server.Handlers.Index;

public class CreateTidingHandler : IRequestHandler<CreateTidingRequest, CreateTidingResponse>
{
    private readonly ITidingService _tidingService;
    
    public CreateTidingHandler(ITidingService tidingService)
    {
        _tidingService = tidingService;
    }
    
    public async Task<CreateTidingResponse> Handle(CreateTidingRequest request, CancellationToken cancellationToken)
    {
        await _tidingService.CreateTidingAsync(request.Title, request.Text);

        return new CreateTidingResponse();
    }
}