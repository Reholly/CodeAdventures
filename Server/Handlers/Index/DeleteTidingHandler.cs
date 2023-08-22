using AutoMapper;
using MediatR;
using Server.Services.TidingServices;
using Shared.Requests.Index;
using Shared.Responses.Index;

namespace Server.Handlers.Index;

public class DeleteTidingHandler : IRequestHandler<DeleteTidingRequest, DeleteTidingResponse>
{
    private readonly ITidingService _tidingService;
    private readonly IMapper _mapper;
    
    public DeleteTidingHandler(ITidingService tidingService, IMapper mapper)
    {
        _tidingService = tidingService;
        _mapper = mapper;
    }
    
    public async Task<DeleteTidingResponse> Handle(DeleteTidingRequest request, CancellationToken cancellationToken)
    {
        var allTidings = await _tidingService.GetTidingsAsync();

        var deletedTiding = await _tidingService.GetTidingByPublicationDateAsync(request.PublicationDate);
        await _tidingService.DeleteTidingAsync(deletedTiding);

        return new DeleteTidingResponse();
    }
}