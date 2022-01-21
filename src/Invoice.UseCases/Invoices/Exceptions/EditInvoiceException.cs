using System;

namespace Invoice.UseCases.Invoices
{
    public class EditInvoiceException : Exception
    {
        public EditInvoiceException(string message) : base(message)
        {
        }
    }
}
