using Invoice.UseCases.Invoices.Helpers;
using Invoice.UseCases.Invoices.ViewModels;
using Invoice.UseCases.Shared.QueryProcessor;
using System.Linq;

namespace Invoice.UseCases.Invoices
{
    public class GetInvoicesUseCase : IGetInvoicesUseCase
    {
        private readonly IInvoiceRepository _repository;
        private readonly IQueryProcessor<GetInvoiceViewModel> _queryProcessor;
        private const int _maxPageSize = 10;

        public GetInvoicesUseCase(IInvoiceRepository repository, IQueryProcessor<GetInvoiceViewModel> queryProcessor)
        {
            this._repository = repository;
            this._queryProcessor = queryProcessor;
        }

        public InvoiceTableViewModel Execute(QueryModel model)
        {
            if (!model.PageSize.HasValue || model.PageSize.Value > _maxPageSize
                || model.PageSize.Value == 0)
            {
                model.PageSize = _maxPageSize;
            }

            var invoices = _repository.GetAll();
            var viewModels = invoices.Select(x => new GetInvoiceViewModel()
            {
                Number = x.Number,
                CreatedAt = x.CreatedAt,
                ProcessingStatus = x.ProcessingStatus.ToViewModelString(),
                Amount = x.Amount,
                PaymentMethod = x.PaymentMethod.ToViewModelString()
            }).AsQueryable();

            var (data, pagesCount) = _queryProcessor.Apply(model, viewModels);
            return new InvoiceTableViewModel()
            {
                Data = data.ToList(),
                PagesCount = pagesCount
            };
        }
    }
}
