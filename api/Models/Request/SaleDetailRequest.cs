namespace Api.Models.Request
{
    public class SaleDetailRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class SaleCreateRequest
    {
        public string Description { get; set; } = default!;
        public List<SaleDetailRequest> Details { get; set; } = new();
    }
}
