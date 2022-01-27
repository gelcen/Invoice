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
