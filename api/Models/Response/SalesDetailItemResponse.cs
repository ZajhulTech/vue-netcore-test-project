namespace Api.Models.Response1
{
    public class SalesDetailItemResponse
    {
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }
}
