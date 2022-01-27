using Invoice.UseCases.Invoices.ViewModels;
using Invoice.UseCases.Shared.QueryProcessor;

namespace Invoice.UseCases.Invoices
{
    public interface IGetInvoicesUseCase
    {
        InvoiceTableViewModel Execute(QueryModel model);
    }
}