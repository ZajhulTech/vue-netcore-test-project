using System.Net;
using Api.Infrastructure.Models;
using Api.Models;
using Api.Models.Request;
using Api.Models.Response1;
using common;
using interfaces.DataBase;
using Interfaces.UserStory;
using Microsoft.EntityFrameworkCore;

namespace Api.UserStories.domain
{
    public class SalesUserStory(IMyUnitOfWork unitOfWork) : IsalesUserStory
    {
        private readonly IMyUnitOfWork unitOfWork = unitOfWork;

      
        public async Task<Response<PagedResponse<SalesGroupedResponse>>> GetAllSalesDetail(PaginationRequest request)
        {
            var result = new Response<PagedResponse<SalesGroupedResponse>>();
            var data = new PagedResponse<SalesGroupedResponse>();

            var saleDetailModel = unitOfWork.Repository<VwSaleDetail>();


            var salesModel = unitOfWork.Repository<Sale>();

            data.TotalRecords = await salesModel.CountAsync();

            var salesList = await salesModel.SearchPageAsync(
                pageIndex: request.pageIndex,
                pageSize: request.pageSize,
                predicate: x => true,
                orderby: x => x.Id,
                desc: true).ConfigureAwait(false);

            if (salesList == null || !salesList.Items.Any())
            {
                result.StatusCode = (int)HttpStatusCode.NotFound;
                result.Message = "No se encontraron ventas.";
                return result;
            }

            var saleIds = salesList.Items
             .Select(s => s.Id)
             .Distinct()
             .ToList();

            var saleDetails = await saleDetailModel.SearchAsync(d => saleIds.Contains(d.SaleId)).ConfigureAwait(false);


            if (saleDetails == null || !saleDetails.Any())
            {
                result.StatusCode = (int)HttpStatusCode.NotFound;
                result.Message = "No se encontraron detalles de ventas.";
                return result;
            }


            data.Raw = salesList.Items
                .Select(s => new SalesGroupedResponse
                {
                    SaleId = s.Id,
                    Description = s.Description,
                    Amount = s.Amount,
                    Date = s.Date,
                    CreatedBy = s.CreatedBy,
                    Detail = saleDetails
                        .Where(d => d.SaleId == s.Id)
                        .Select(d => new SalesDetailItemResponse
                        {
                            ProductName = d.ProductName,
                            Quantity = d.Quantity,
                            UnitPrice = d.UnitPrice,
                            Subtotal = d.Subtotal ?? 0
                        })
                        .ToList()
                })
                .ToList();

            data.TotalPages = (int)Math.Ceiling((double)data.TotalRecords / request.pageSize);
            if (data.TotalPages == 0 && data.Raw.Any())
            {
                data.TotalPages = 1;
            }

            result.Payload = data;
            result.StatusCode = 200;
            result.Message = "OK";
            return result;
        }


        public async Task<Response<SaleResponse>> CreateSaleAsync(SaleCreateRequest request)
        {
            var response = new Response<SaleResponse>();

            if (request == null || request.Details == null || !request.Details.Any())
            {
                response.StatusCode = 400;
                response.Message = "Debe incluir al menos un detalle de venta.";
                return response;
            }


            var saleRepo = unitOfWork.Repository<Sale>();
            var saleDetailRepo = unitOfWork.Repository<SaleDetail>();
            try { 
                await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);

                // created Sale entitie
                decimal amount = request.Details.Sum(d => d.Quantity * d.UnitPrice);

                var saleModel = await saleRepo.AddAsync(new Sale()
                {
                    Description = request.Description,
                    Amount = amount,
                    Date = DateTime.Now,
                    CreatedBy = "api",
                }).ConfigureAwait(false);

                await saleRepo.AddAsync(saleModel).ConfigureAwait(false);
                await unitOfWork.SaveChangesAsync().ConfigureAwait(false);

                foreach (var item in request.Details)
                {

                    var detail = new SaleDetail
                    {
                        SaleId = saleModel.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                    };
                    await saleDetailRepo.AddAsync(detail).ConfigureAwait(false);

                }

                await unitOfWork.SaveChangesAsync().ConfigureAwait(false);

                await unitOfWork.CommitAsync().ConfigureAwait(false);

                var data = new SaleResponse()
                {
                    Id = saleModel.Id,
                    Amount = saleModel.Amount,
                    Description = saleModel.Description,

                };

                response.StatusCode = 201;
                response.Message = "Venta creada correctamente";
                response.Payload = data;
                return response;
            }
            catch (Exception ex)
            {

                await unitOfWork.RollbackAsync().ConfigureAwait(false);

                response.StatusCode = 500;
                response.Message = $"Error al crear la venta: {ex.Message}";
                return response;
            }

        }

    }

}
