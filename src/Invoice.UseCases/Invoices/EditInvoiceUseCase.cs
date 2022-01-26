using Invoice.CoreBusiness;
using Invoice.UseCases.Invoices.InputDtos;
using System;
using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public class EditInvoiceUseCase : IEditInvoiceUseCase
    {
        private readonly IInvoiceRepository _repository;

        public EditInvoiceUseCase(IInvoiceRepository repository)
        {
            this._repository = repository;
        }

        public async Task Execute(EditInvoiceDto dto)
        {
            if (dto.Number == null || dto.Amount == null || dto.PaymentMethod == null)
            {
                throw new EditInvoiceException("Number, Amount and PaymentMethod of invoice should not be null");
            }

            var invoice = await _repository.GetByNumber(dto.Number.Value);

            invoice.Number = dto.Number.Value;
            invoice.Amount = dto.Amount.Value;
            invoice.PaymentMethod = (PaymentMethod)dto.PaymentMethod;
            invoice.ModifiedAt = DateTime.Now;

            await _repository.UpdateInvoice(invoice);
        }
    }
}
