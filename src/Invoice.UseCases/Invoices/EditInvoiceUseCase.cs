using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public class EditInvoiceUseCase : IEditInvoiceUseCase
    {
        private readonly IInvoiceRepository _repository;
        private readonly IGetInvoiceByIdUseCase _getInvoiceByIdUseCase;

        public EditInvoiceUseCase(IInvoiceRepository repository, IGetInvoiceByIdUseCase getInvoiceByIdUseCase)
        {
            this._repository = repository;
            this._getInvoiceByIdUseCase = getInvoiceByIdUseCase;
        }

        public async Task Execute(int? invoiceId, float? amount)
        {
            if (invoiceId == null || amount == null)
            {
                throw new EditInvoiceException("Id and Amount of invoice should not be null");
            }

            var invoice = await _getInvoiceByIdUseCase.Execute(invoiceId.Value);

            invoice.Amount = amount.Value;

            await _repository.UpdateInvoice(invoice);
        }
    }
}
