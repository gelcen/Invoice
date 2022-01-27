using System.Collections.Generic;

namespace Invoice.UseCases.Invoices.ViewModels
{
    public class InvoiceTableViewModel
    {
        public int PagesCount { get; set; }
        public List<GetInvoiceViewModel> Data { get; set; }
    }
}
