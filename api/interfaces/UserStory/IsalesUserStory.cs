using Api.Models;
using Api.Models.Request;
using Api.Models.Response1;

namespace Interfaces.UserStory
{
    public interface IsalesUserStory
    {
        Task<Response<PagedResponse<SalesGroupedResponse>>> GetAllSalesDetail(PaginationRequest request);
        Task<Response<SaleResponse>> CreateSaleAsync(SaleCreateRequest request);
    }
}