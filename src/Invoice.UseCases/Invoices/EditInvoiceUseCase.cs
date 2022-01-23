using System;
using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public class EditInvoiceUseCase : IEditInvoiceUseCase
    {
        private readonly IInvoiceRepository _repository;
        private readonly IGetInvoiceByNumberUseCase _getInvoiceByNumberUseCase;

        public EditInvoiceUseCase(IInvoiceRepository repository, IGetInvoiceByNumberUseCase getInvoiceByNumberUseCase)
        {
            this._repository = repository;
            this._getInvoiceByNumberUseCase = getInvoiceByNumberUseCase;
        }

        public async Task Execute(int? number, float? amount)
        {
            if (number == null || amount == null)
            {
                throw new EditInvoiceException("Number and Amount of invoice should not be null");
            }

            var invoice = await _getInvoiceByNumberUseCase.Execute(number.Value);

            invoice.Amount = amount.Value;
            invoice.ModifiedAt = DateTime.Now;

            await _repository.UpdateInvoice(invoice);
        }
    }
}
