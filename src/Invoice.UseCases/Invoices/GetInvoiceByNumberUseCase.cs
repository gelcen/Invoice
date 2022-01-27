using Invoice.UseCases.Invoices.Helpers;
using Invoice.UseCases.Invoices.ViewModels;
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

        public async Task<GetInvoiceByNumberViewModel> Execute(int number)
        {
            var invoice = await _repository.GetByNumber(number);

            if (invoice == null)
            {
                throw new InvoiceNotFoundException($"Invoice with number {number} not found");
            }

            return new GetInvoiceByNumberViewModel()
            {
                Number = invoice.Number,
                PaymentMethod = (int)invoice.PaymentMethod,
                Amount = invoice.Amount
            };
        }
    }
}
