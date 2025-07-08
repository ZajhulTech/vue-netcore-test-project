using Api.Models.Request;
using Interfaces.UserStory;
using Microsoft.AspNetCore.Mvc;
using Api.infrastructure.Api;
using Api.UserStories.domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ventasController(IsalesUserStory salesUserStory) : ControllerBase
    {
        private readonly IsalesUserStory salesUserStory = salesUserStory;

        [HttpGet]
        public async Task<IActionResult> getSales([FromQuery] PaginationRequest request)
        {
            if (request == null)
                return BadRequest("Parametro de entrada nulo");

            var response = await salesUserStory.GetAllSalesDetail(request).ConfigureAwait(false);
            return response.GetActionResult();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CreatePolicy")] // aply custom Politic
        public async Task<IActionResult> CreateSale([FromBody] SaleCreateRequest request)
        {
            var response = await salesUserStory.CreateSaleAsync(request)
                                     .ConfigureAwait(false);
            return response.GetActionResult();
        }
    }
}
