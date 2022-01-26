using Invoice.CoreBusiness;
using Invoice.UseCases.Invoices.InputDtos;
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

        public async Task<CoreBusiness.Invoice> Execute(AddInvoiceDto dto)
        {
            if (dto.Number == null || dto.Amount == null || dto.PaymentMethod == null)
            {
                throw new AddInvoiceException("Number, Amount and PaymentMethod of invoice should not be null");
            }

            if (dto.PaymentMethod.Value < 1 || dto.PaymentMethod.Value > 3)
            {
                throw new AddInvoiceException("Incorrect value for PaymentMethod. Should be in range from 1 to 3");
            }

            var invoiceWithNumber = await _repository.GetByNumber(dto.Number.Value);

            if (invoiceWithNumber != null)
            {
                throw new AddInvoiceException("There is already invoice with that Number");
            }

            var invoice = new CoreBusiness.Invoice()
            {
                Number = dto.Number.Value,
                Amount = dto.Amount.Value,
                PaymentMethod = (PaymentMethod)dto.PaymentMethod.Value
            };

            await _repository.AddInvoice(invoice);

            return invoice;
        }
    }
}
