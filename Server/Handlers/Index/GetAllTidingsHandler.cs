using AutoMapper;
using Data.Entities;
using MediatR;
using Server.Services.TidingServices;
using Shared.DTO;
using Shared.Requests.Index;
using Shared.Responses.Index;

namespace Server.Handlers.Index;

public class GetAllTidingsHandler : IRequestHandler<GetAllTidingsRequest, GetAllTidingsResponse>
{
    private readonly ITidingService _tidingService;
    private readonly IMapper _mapper;
    
    public GetAllTidingsHandler(ITidingService tidingService, IMapper mapper)
    {
        _tidingService = tidingService;
        _mapper = mapper;
    }

    public async Task<GetAllTidingsResponse> Handle(GetAllTidingsRequest request, CancellationToken cancellationToken)
    {
        var tidings = await _tidingService.GetTidingsAsync();
        var tidingsDtos = tidings
            .Select(x => _mapper.Map<Tiding, TidingModel>(x))
            .ToArray();

        return new GetAllTidingsResponse { Tidings = tidingsDtos };
    }
}