using Invoice.UseCases.Invoices.ViewModels;
using Invoice.UseCases.Shared.QueryProcessor;
using Sieve.Models;
using Sieve.Services;
using System.Collections.Generic;
using System.Linq;

namespace Invoice.Plugins.QueryProcessor.Sieve
{
    public class QueryProcessor : IQueryProcessor<GetInvoiceViewModel>
    {
        private readonly ISieveProcessor _sieveProcessor;

        public QueryProcessor(ISieveProcessor sieveProcessor)
        {
            this._sieveProcessor = sieveProcessor;
        }

        public (IQueryable<GetInvoiceViewModel>, int) Apply2(QueryModel model, IQueryable<GetInvoiceViewModel> source)
        {
            var sieveModel = new SieveModel()
            {
                Filters = model.Filters,
                Sorts = model.Sorts,
                Page = model.Page,
                PageSize = model.PageSize
            };
            var modelWithoutSize = new SieveModel()
            {
                Filters = sieveModel.Filters,
                Sorts = sieveModel.Sorts,
                Page = sieveModel.Page,
                PageSize = null
            };
            var result = _sieveProcessor.Apply(modelWithoutSize, source).ToList();
            int count;
            if (string.IsNullOrEmpty(sieveModel.Filters) || string.IsNullOrWhiteSpace(sieveModel.Filters))
            {
                count = source.Count();
                result = _sieveProcessor.Apply(sieveModel, source).ToList();
            }
            else
            {
                //result = SieveFilter(sieveModel, result);
                result = OwnFilter(sieveModel.Filters, result);
                count = result.Count();
                //result = _sieveProcessor.Apply(new SieveModel()
                //{
                //    Filters = null,
                //    Sorts = null,
                //    Page = null,
                //    PageSize = null
                //}, result.AsQueryable()).ToList();
                result = _sieveProcessor.Apply(sieveModel, result.AsQueryable()).ToList();
            }
            //result = _sieveProcessor.Apply(sieveModel, result.AsQueryable()).ToList();
            int pagesCount = count / sieveModel.PageSize.Value;
            if (count % sieveModel.PageSize.Value != 0)
            {
                pagesCount++;
            }

            return (result.AsQueryable(), pagesCount);
        }

        private List<GetInvoiceViewModel> SieveFilter(SieveModel model, List<GetInvoiceViewModel> source)
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

        private List<GetInvoiceViewModel> OwnFilter(string filter, List<GetInvoiceViewModel> source)
        {
            return source.Where(x => x.Number.ToString().Contains(filter)
            || x.CreatedAt.ToString("dd.MM.yyyy H:mm").Contains(filter)
            || x.ProcessingStatus.Contains(filter)
            || x.Amount.ToString().Contains(filter)
            || x.PaymentMethod.Contains(filter)).ToList();
        }

        public (IQueryable<GetInvoiceViewModel>, int) Apply(QueryModel model, IQueryable<GetInvoiceViewModel> source)
        {
            IQueryable<GetInvoiceViewModel> sortedResult = null;
            if (!string.IsNullOrEmpty(model.Sorts) && !string.IsNullOrWhiteSpace(model.Sorts))
            {
                sortedResult = _sieveProcessor.Apply(new SieveModel()
                {
                    Filters = null,
                    Sorts = model.Sorts,
                    Page = null,
                    PageSize = null
                }, source);
            }

            List<GetInvoiceViewModel> filteredResult = null;
            if (!string.IsNullOrEmpty(model.Filters) && !string.IsNullOrWhiteSpace(model.Filters))
            {
                if (sortedResult != null)
                {
                    filteredResult = OwnFilter(model.Filters, sortedResult.ToList());
                }
                else
                {
                    filteredResult = OwnFilter(model.Filters, source.ToList());
                }
            }

            if (!model.Page.HasValue || model.Page == 0)
            {
                model.Page = 1;
            }

            int count;
            IQueryable<GetInvoiceViewModel> pagedResult;
            if (filteredResult != null)
            {
                count = filteredResult.Count;
                pagedResult = filteredResult.Skip((model.Page.Value - 1) * model.PageSize.Value).Take(model.PageSize.Value).AsQueryable();
            }
            else
            {
                if (sortedResult != null)
                {
                    count = sortedResult.Count();
                    pagedResult = sortedResult.Skip((model.Page.Value - 1) * model.PageSize.Value).Take(model.PageSize.Value);
                }
                else
                {
                    count = source.Count();
                    pagedResult = source.Skip((model.Page.Value - 1) * model.PageSize.Value).Take(model.PageSize.Value);
                }
            }

            int pagesCount = count / model.PageSize.Value;
            if (count % model.PageSize.Value != 0)
            {
                pagesCount++;
            }

            return (pagedResult, pagesCount);
        }
    }
}
