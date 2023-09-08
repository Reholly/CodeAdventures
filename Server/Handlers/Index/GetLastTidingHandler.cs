using AutoMapper;
using Data.Entities;
using MediatR;
using Server.Exceptions;
using Server.Services.TidingServices;
using Shared.DTO;
using Shared.Requests.Index;
using Shared.Responses.Index;

namespace Server.Handlers.Index;

public class GetLastTidingHandler : IRequestHandler<GetLastTidingRequest, GetLastTidingResponse>
{
    private readonly ITidingService _tidingService;
    private readonly IMapper _mapper;
    
    public GetLastTidingHandler(ITidingService tidingService, IMapper mapper)
    {
        _tidingService = tidingService;
        _mapper = mapper;
    }

    public async Task<GetLastTidingResponse> Handle(GetLastTidingRequest request, CancellationToken cancellationToken)
    {
        var lastTiding = await _tidingService.GetLastTidingsAsync()
            ?? throw new ServiceInvalidOperationException("");
        
        var tidingDto = _mapper.Map<Tiding, TidingModel>(lastTiding);
        
        return new GetLastTidingResponse { Tiding = tidingDto };
    }
}