using System;

namespace Invoice.CoreBusiness
{
    public class Invoice
    {
        public int Number { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        public ProcessingStatus ProcessingStatus { get; set; } = ProcessingStatus.New;
        public float Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
