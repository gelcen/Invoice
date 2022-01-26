namespace Invoice.UseCases.Invoices.ViewModels
{
    public class GetInvoiceByNumberViewModel
    {
        public int Number { get; set; }
        public float Amount { get; set; }
        public int PaymentMethod { get; set; }
    }
}
