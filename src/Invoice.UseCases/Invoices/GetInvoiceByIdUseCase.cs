using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public class GetInvoiceByIdUseCase : IGetInvoiceByIdUseCase
    {
        private readonly IInvoiceRepository _repository;

        public GetInvoiceByIdUseCase(IInvoiceRepository repository)
        {
            this._repository = repository;
        }

        public async Task<CoreBusiness.Invoice> Execute(int invoiceId)
        {
            var invoice = await _repository.GetById(invoiceId);

            if (invoice == null)
            {
                throw new InvoiceNotFoundException($"Invoice with ID {invoiceId} not found");
            }

            return invoice;
        }
    }
}
