using System;

namespace Invoice.UseCases.Invoices.ViewModels
{
    public class GetInvoiceViewModel
    {
        public int Number { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProcessingStatus { get; set; }
        public float Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
