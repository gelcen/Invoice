using Invoice.UseCases.Invoices.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Invoice.UseCases.Invoices.Helpers;
using Sieve.Services;
using Sieve.Models;
using System.Text;

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

            //PrepareFilteringInModel(sieveModel);

            var invoices = _repository.GetAll();
            var viewModels = invoices.Select(x => new GetInvoiceViewModel()
            {
                Number = x.Number,
                CreatedAt = x.CreatedAt,
                ProcessingStatus = x.ProcessingStatus.ToViewModelString(),
                Amount = x.Amount,
                PaymentMethod = x.PaymentMethod.ToViewModelString()
            }).AsQueryable();
            var modelWithoutSize = new SieveModel()
            {
                Filters = sieveModel.Filters,
                Sorts = sieveModel.Sorts,
                Page = sieveModel.Page,
                PageSize = null
            };
            var result = _sieveProcessor.Apply(modelWithoutSize, viewModels).ToList();
            int count;
            if (string.IsNullOrEmpty(sieveModel.Filters) || string.IsNullOrWhiteSpace(sieveModel.Filters))
            {
                count = invoices.Count();
                result = _sieveProcessor.Apply(sieveModel, viewModels).ToList();
            }
            else
            {
                result = FilterAllFields(sieveModel, result);
                count = result.Count();
                result = _sieveProcessor.Apply(sieveModel, result.AsQueryable()).ToList();
            }
            //result = _sieveProcessor.Apply(sieveModel, result.AsQueryable()).ToList();
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

        private SieveModel PrepareFilteringInModel(SieveModel model)
        {
            if (!string.IsNullOrEmpty(model.Filters) && !string.IsNullOrWhiteSpace(model.Filters))
            {
                StringBuilder sb = new StringBuilder("");
                //sb.Append("number@=*" + model.Filters + ",");
                //sb.Append("amount@=*" + model.Filters + ",");
                //sb.Append("createdAt@=*" + model.Filters + ",");
                sb.Append("processingStatus@=*" + model.Filters + "| " + ",");
                sb.Append("paymentMethod@=*" + model.Filters + "| ");
                model.Filters = sb.ToString();
            }
            return model;
        }

        private List<GetInvoiceViewModel> FilterAllFields(SieveModel model, List<GetInvoiceViewModel> source)
        {
            if (!string.IsNullOrEmpty(model.Filters) && !string.IsNullOrWhiteSpace(model.Filters))
            {
                int pageSize = model.PageSize.Value;
                string filter = model.Filters;
                model.PageSize = null;

                var numberResult = GetFilteredItems("number@=*" + filter, model, source);

                var amountResult = GetFilteredItems("amount@=*" + filter, model, source);
                
                var createdAtResult = GetFilteredItems("createdAt@=*" + filter, model, source);

                var processingStatusResult = GetFilteredItems("processingStatus@=*" + filter, model, source);

                var paymentMethodResult = GetFilteredItems("paymentMethod@=*" + filter, model, source);

                var result = numberResult.Union(amountResult.Union(createdAtResult.Union(processingStatusResult.Union(
                    paymentMethodResult)))).ToList();

                model.Filters = null;
                model.PageSize = pageSize;
                
                return result;
            }

            source = _sieveProcessor.Apply(model, source.AsQueryable()).ToList();

            return source;
        }

        private List<GetInvoiceViewModel> GetFilteredItems(string filter, SieveModel model, List<GetInvoiceViewModel> source)
        {
            model.Filters = filter;
            var result = _sieveProcessor.Apply(model, source.AsQueryable()).ToList();
            if (result.Count == source.Count)
                result.Clear();
            return result;
        }
    }
}
