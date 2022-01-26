namespace Invoice.UseCases.Invoices.InputDtos
{
    public class EditInvoiceDto
    {
        public int? Number { get; set; }
        public float? Amount { get; set; }
        public int? PaymentMethod { get; set; }
    }
}
