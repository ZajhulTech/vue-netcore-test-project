
using Api.Infrastructure.Models;

using Api.Models.Response1;
using common;
using interfaces.DataBase;
using Api.Models;
using System.Net;
using Interfaces.UserStory;

namespace Api.UserStories.domain
{
    public class CatalogUserStory(IMyUnitOfWork unitOfWork) : ICatalogUserStory
    {

        private readonly IMyUnitOfWork unitOfWork = unitOfWork;

        public async Task<Response<List<ProductResponse>>> GetProducts()
        {
            var result = new Response<List<ProductResponse>>();

            var productRepo = unitOfWork.Repository<Product>();
            var productEntities = await productRepo.SearchAllAsync().ConfigureAwait(false);

            if (productEntities == null || !productEntities.Any())
            {

                result.StatusCode = (int)HttpStatusCode.NoContent;
                result.Message = "No se encontraron productos.";
                return result;
            }

            var productList = productEntities.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,

            }).ToList();

            result.Payload = productList;
            result.StatusCode = 200;
            result.Message = "OK";
            return result;
        }
    }
}
