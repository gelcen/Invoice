using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public class GetInvoiceByNumberUseCase : IGetInvoiceByNumberUseCase
    {
        private readonly IInvoiceRepository _repository;

        public GetInvoiceByNumberUseCase(IInvoiceRepository repository)
        {
            this._repository = repository;
        }

        public async Task<CoreBusiness.Invoice> Execute(int number)
        {
            var invoice = await _repository.GetByNumber(number);

            if (invoice == null)
            {
                throw new InvoiceNotFoundException($"Invoice with number {number} not found");
            }

            return invoice;
        }
    }
}
