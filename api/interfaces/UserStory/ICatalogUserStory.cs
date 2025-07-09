using Api.Models;
using Api.Models.Response1;

namespace Interfaces.UserStory
{
    public interface ICatalogUserStory
    {
        Task<Response<List<ProductResponse>>> GetProducts();
    }
}