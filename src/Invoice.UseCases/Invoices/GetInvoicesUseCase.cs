using Invoice.UseCases.Invoices.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Invoice.UseCases.Invoices.Helpers;

namespace Invoice.UseCases.Invoices
{
    public class GetInvoicesUseCase : IGetInvoicesUseCase
    {
        private readonly IInvoiceRepository _repository;

        public GetInvoicesUseCase(IInvoiceRepository repository)
        {
            this._repository = repository;
        }

        public async Task<List<GetInvoiceViewModel>> Execute()
        {
            var invoices = await _repository.GetAll();
            return invoices.Select(x => new GetInvoiceViewModel() 
            { 
                Number = x.Number,
                CreatedAt = x.CreatedAt,
                ProcessingStatus = x.ProcessingStatus.ToViewModelString(),
                Amount = x.Amount,
                PaymentMethod = x.PaymentMethod.ToViewModelString()
            }).ToList();
        }
    }
}
