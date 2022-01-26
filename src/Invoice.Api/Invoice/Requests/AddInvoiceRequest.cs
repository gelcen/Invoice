namespace Invoice.Api.Controllers
{
    public class AddInvoiceRequest
    {
        public int? Number { get; set; }
        public float? Amount { get; set; }
        public int? PaymentMethod { get; set; }
    }
}
