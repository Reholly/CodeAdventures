using MediatR;
using Server.Services.TidingServices;
using Shared.Requests.Index;
using Shared.Responses.Index;

namespace Server.Handlers.Index;

public class UpdateTidingHandler : IRequestHandler<UpdateTidingRequest, UpdateTidingResponse>
{
    private readonly ITidingService _tidingService;

    public UpdateTidingHandler(ITidingService tidingService)
        => _tidingService = tidingService;
    
    public async Task<UpdateTidingResponse> Handle(UpdateTidingRequest request, CancellationToken cancellationToken)
    {
        await _tidingService.UpdateTidingAsync(
            request.Title, 
            request.Text, 
            request.PublicationDate);

        return new UpdateTidingResponse();
    }
}