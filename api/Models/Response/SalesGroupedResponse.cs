namespace Api.Models.Response1
{
    public class SalesGroupedResponse
    {
        public int SaleId { get; set; }
        public string Description { get; set; } = default!;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; } = default!;
        public List<SalesDetailItemResponse> Detail { get; set; } = new();
    }

}
