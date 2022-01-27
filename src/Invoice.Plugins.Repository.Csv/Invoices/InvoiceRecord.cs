using CsvHelper.Configuration.Attributes;
using System;

namespace Invoice.Plugins.Repository.Csv.Invoices
{
    public class InvoiceRecord
    {
        [Index(0)]
        public DateTime CreatedAt { get; set; }
        [Index(1)]
        public int Number { get; set; }
        [Index(2)]
        public int ProcessingStatus { get; set; }
        [Index(3)]
        public string Amount { get; set; }
        [Index(4)]
        public int PaymentMethod { get; set; }
        [Index(5)]
        public DateTime ModifiedAt { get; set; }
    }
}
