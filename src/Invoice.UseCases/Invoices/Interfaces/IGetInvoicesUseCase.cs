using Invoice.UseCases.Invoices.ViewModels;
using Sieve.Models;

namespace Invoice.UseCases.Invoices
{
    public interface IGetInvoicesUseCase
    {
        InvoiceTableViewModel Execute(SieveModel sieveModel);
    }
}