using System;

namespace Invoice.UseCases.Invoices
{
    public class AddInvoiceException : Exception
    {
        public AddInvoiceException(string message) : base(message)
        {
        }
    }
}
