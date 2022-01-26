using Sieve.Attributes;
using System;

namespace Invoice.CoreBusiness
{
    public class Invoice
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Number { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        [Sieve(CanFilter = true, CanSort = true)]
        public ProcessingStatus ProcessingStatus { get; set; } = ProcessingStatus.New;
        [Sieve(CanFilter = true, CanSort = true)]
        public float Amount { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
