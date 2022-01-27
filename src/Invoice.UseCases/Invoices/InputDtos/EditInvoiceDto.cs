namespace Invoice.UseCases.Invoices.InputDtos
{
    public class EditInvoiceDto
    {
        public int? PreviousNumber { get; set; }
        public int? Number { get; set; }
        public float? Amount { get; set; }
        public int? PaymentMethod { get; set; }
    }
}
