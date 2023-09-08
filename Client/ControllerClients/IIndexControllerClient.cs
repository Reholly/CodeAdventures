using Refit;
using Shared.Requests.Index;
using Shared.Responses.Index;

namespace Client.ControllerClients;

public interface IIndexControllerClient
{
    [Post("/index/create")]
    Task<ApiResponse<CreateTidingResponse>> CreateTiding(CreateTidingRequest request);

    [Delete("/index/delete")]
    Task<ApiResponse<DeleteTidingResponse>> DeleteTiding(DeleteTidingRequest request);

    [Get("/index/getall")]
    Task<ApiResponse<GetAllTidingsResponse>> GetAllTidings(GetAllTidingsRequest request);

    [Get("/index/getlast")]
    Task<ApiResponse<GetLastTidingResponse>> GetLastTiding(GetLastTidingRequest request);

    [Put("/index/updatetiding")]
    Task<ApiResponse<UpdateTidingResponse>> UpdateTiding(UpdateTidingRequest request);
}