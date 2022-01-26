using Invoice.UseCases.Invoices.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Invoice.UseCases.Invoices.Helpers;
using Sieve.Services;
using Sieve.Models;

namespace Invoice.UseCases.Invoices
{
    public class GetInvoicesUseCase : IGetInvoicesUseCase
    {
        private readonly IInvoiceRepository _repository;
        private readonly SieveProcessor _sieveProcessor;
        private const int _maxPageSize = 10;

        public GetInvoicesUseCase(IInvoiceRepository repository, SieveProcessor sieveProcessor)
        {
            this._repository = repository;
            this._sieveProcessor = sieveProcessor;
        }

        public InvoiceTableViewModel Execute(SieveModel sieveModel)
        {
            if (!sieveModel.PageSize.HasValue || sieveModel.PageSize.Value > _maxPageSize
                || sieveModel.PageSize.Value == 0)
            {
                sieveModel.PageSize = _maxPageSize;
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
            var result = _sieveProcessor.Apply(sieveModel, viewModels).ToList();
            int count = invoices.Count();
            int pagesCount = count / sieveModel.PageSize.Value;
            if (count % sieveModel.PageSize.Value != 0)
            {
                pagesCount++;
            }
            return new InvoiceTableViewModel()
            {
                Data = result,
                PagesCount = pagesCount
            };
        }
    }
}
