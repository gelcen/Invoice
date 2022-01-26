using Sieve.Attributes;
using System;

namespace Invoice.UseCases.Invoices.ViewModels
{
    public class GetInvoiceViewModel
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Number { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime CreatedAt { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string ProcessingStatus { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public float Amount { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string PaymentMethod { get; set; }
    }
}
