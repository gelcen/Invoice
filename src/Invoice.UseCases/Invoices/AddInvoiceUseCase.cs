using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public class AddInvoiceUseCase : IAddInvoiceUseCase
    {
        private readonly IInvoiceRepository _repository;

        public AddInvoiceUseCase(IInvoiceRepository repository)
        {
            this._repository = repository;
        }

        public async Task<CoreBusiness.Invoice> Execute(int? number, float? amount)
        {
            if (number == null || amount == null)
            {
                throw new AddInvoiceException("Number and Amount of invoice should not be null");
            }

            var invoiceWithNumber = await _repository.GetByNumber(number.Value);

            if (invoiceWithNumber != null)
            {
                throw new AddInvoiceException("There is already invoice with that Number");
            }

            var invoice = new CoreBusiness.Invoice()
            {
                Number = number.Value,
                Amount = amount.Value
            };

            await _repository.AddInvoice(invoice);

            return invoice;
        }
    }
}
