namespace Invoice.Api.Invoice.Requests
{
    public class UpdateInvoiceRequest
    {
        public int? Number { get; set; }
        public float? Amount { get; set; }
        public int? PaymentMethod { get; set; }
    }
}
