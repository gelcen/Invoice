using Invoice.UseCases.Invoices.Exceptions;

namespace Invoice.UseCases.Invoices
{
    public class AddInvoiceException : BadRequestException
    {
        public AddInvoiceException(string message) : base(message)
        {
        }
    }
}
