using Interfaces.UserStory;
using Microsoft.AspNetCore.Mvc;
using Api.infrastructure.Api;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class catalogController(ICatalogUserStory catalogUserStory) : ControllerBase
    {
        private readonly ICatalogUserStory catalogUserStory = catalogUserStory;

        [HttpGet("products")]
        public async Task<IActionResult> getProducts()
        {
            
            var response = await catalogUserStory.GetProducts().ConfigureAwait(false);
            return response.GetActionResult();
        }

    }
}
