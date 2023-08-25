using AutoMapper;
using MediatR;
using Server.Services.TidingServices;
using Shared.DTO;
using Shared.Requests.Index;
using Shared.Responses.Index;

namespace Server.Handlers.Index;

public class UpdateTidingHandler : IRequestHandler<UpdateTidingRequest, UpdateTidingResponse>
{
    private readonly ITidingService _tidingService;
    private readonly IMapper _mapper;

    public UpdateTidingHandler(ITidingService tidingService, IMapper mapper)
    {
        _tidingService = tidingService;
        _mapper = mapper;
    }

    public async Task<UpdateTidingResponse> Handle(UpdateTidingRequest request, CancellationToken cancellationToken)
    {
        var updatedTiding = await _tidingService.UpdateTidingAsync(
            request.Title, 
            request.Text, 
            request.PublicationDate);

        return new UpdateTidingResponse { UpdatedTiding = _mapper.Map<TidingModel>(updatedTiding) };
    }
}