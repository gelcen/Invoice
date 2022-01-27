using Invoice.UseCases.Invoices.ViewModels;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace Invoice.Plugins.QueryProcessor.Sieve
{
    public class InvoiceSieveProcessor : SieveProcessor
    {
        public InvoiceSieveProcessor(IOptions<SieveOptions> options)
        : base(options)
        {
        }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<GetInvoiceViewModel>(p => p.Number)
                .CanFilter()
                .CanSort();

            mapper.Property<GetInvoiceViewModel>(p => p.CreatedAt)
                .CanFilter()
                .CanSort();

            mapper.Property<GetInvoiceViewModel>(p => p.ProcessingStatus)
                .CanSort()
                .CanFilter();

            mapper.Property<GetInvoiceViewModel>(p => p.Amount)
                .CanSort()
                .CanFilter();

            mapper.Property<GetInvoiceViewModel>(p => p.PaymentMethod)
                .CanSort()
                .CanFilter();

            return mapper;
        }
    }
}
