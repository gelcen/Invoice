using System;

namespace Invoice.Plugins.Repository.Csv.Invoices
{
    public class InvoiceRecord
    {
        public DateTime Created { get; set; }
        public int Id { get; set; }
        public int ProcessingStatus { get; set; }
        public string Amount { get; set; }
        public int PaymentMethod { get; set; }
    }
}
