using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public interface IGetInvoicesUseCase
    {
        Task<List<CoreBusiness.Invoice>> Execute();
    }
}