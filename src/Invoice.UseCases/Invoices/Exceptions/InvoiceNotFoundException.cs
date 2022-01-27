using Invoice.UseCases.Invoices.Exceptions;

namespace Invoice.UseCases.Invoices
{
    public class InvoiceNotFoundException : NotFoundException
    {
        public InvoiceNotFoundException(string message) : base(message)
        {
        }
    }
}
