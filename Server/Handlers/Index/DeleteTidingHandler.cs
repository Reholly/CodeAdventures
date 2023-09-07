using MediatR;
using Serilog;
using Server.Services.TidingServices;
using Shared.Requests.Index;
using Shared.Responses.Index;

namespace Server.Handlers.Index;

public class DeleteTidingHandler : IRequestHandler<DeleteTidingRequest, DeleteTidingResponse>
{
    private readonly ITidingService _tidingService;
    
    public DeleteTidingHandler(ITidingService tidingService)
    {
        _tidingService = tidingService;
    }
    
    public async Task<DeleteTidingResponse> Handle(DeleteTidingRequest request, CancellationToken cancellationToken)
    {
        var deletedTiding = await _tidingService.GetTidingByPublicationDateAsync(request.PublicationDate);
        await _tidingService.DeleteTidingAsync(deletedTiding);
        
        Log.Information($"Новость от {deletedTiding.PublicationDate} была удалена");

        return new DeleteTidingResponse();
    }
}