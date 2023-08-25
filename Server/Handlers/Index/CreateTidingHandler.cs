using AutoMapper;
using MediatR;
using Server.Services.TidingServices;
using Shared.DTO;
using Shared.Requests.Index;
using Shared.Responses.Index;

namespace Server.Handlers.Index;

public class CreateTidingHandler : IRequestHandler<CreateTidingRequest, CreateTidingResponse>
{
    private readonly ITidingService _tidingService;
    private readonly IMapper _mapper;
    
    public CreateTidingHandler(ITidingService tidingService, IMapper mapper)
    {
        _tidingService = tidingService;
        _mapper = mapper;
    }
    
    public async Task<CreateTidingResponse> Handle(CreateTidingRequest request, CancellationToken cancellationToken)
    {
        var tiding = await _tidingService.CreateTidingAsync(request.Title, request.Text);

        return new CreateTidingResponse { CreatedTiding = _mapper.Map<TidingModel>(tiding) };
    }
}