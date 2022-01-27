using Invoice.UseCases.Invoices.Exceptions;

namespace Invoice.UseCases.Invoices
{
    public class EditInvoiceException : BadRequestException
    {
        public EditInvoiceException(string message) : base(message)
        {
        }
    }
}
