using Invoice.UseCases.Invoices.ViewModels;
using Sieve.Services;
using System.Linq;

namespace Invoice.Api.Invoice.Controllers
{
    public class MyCustomFilterMethods : ISieveCustomFilterMethods
    {
        public IQueryable<GetInvoiceViewModel> Get(IQueryable<GetInvoiceViewModel> source, string op, string[] values)
            => source.Where(x => x.Number > 10);
    }
}
