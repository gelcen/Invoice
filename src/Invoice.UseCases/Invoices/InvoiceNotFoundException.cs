using System;

namespace Invoice.UseCases.Invoices
{
    public class InvoiceNotFoundException : Exception
    {
        public InvoiceNotFoundException(string message) : base(message)
        {
        }
    }
}
