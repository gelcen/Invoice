using Invoice.UseCases.Invoices.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public interface IGetInvoicesUseCase
    {
        Task<List<GetInvoiceViewModel>> Execute();
    }
}