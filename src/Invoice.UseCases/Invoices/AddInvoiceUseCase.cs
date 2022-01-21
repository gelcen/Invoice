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

        public async Task<CoreBusiness.Invoice> Execute(int? invoiceId, float? amount)
        {
            if (invoiceId == null || amount == null)
            {
                throw new AddInvoiceException("Id and Amount of invoice should not be null");
            }

            var invoiceWithId = await _repository.GetById(invoiceId.Value);

            if (invoiceWithId != null)
            {
                throw new AddInvoiceException("There is already invoice with that Id");
            }

            var invoice = new CoreBusiness.Invoice()
            {
                InvoiceId = invoiceId.Value,
                Amount = amount.Value
            };

            await _repository.AddInvoice(invoice);

            return invoice;
        }
    }
}
