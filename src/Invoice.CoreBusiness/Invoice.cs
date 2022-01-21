using System;

namespace Invoice.CoreBusiness
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; }
        public ProcessingStatus ProcessingStatus { get; set; }
        public float Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
