using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public class GetInvoicesUseCase : IGetInvoicesUseCase
    {
        private readonly IInvoiceRepository _repository;

        public GetInvoicesUseCase(IInvoiceRepository repository)
        {
            this._repository = repository;
        }

        public async Task<List<CoreBusiness.Invoice>> Execute()
        {
            var invoices = await _repository.GetAll();
            return invoices;
        }
    }
}
