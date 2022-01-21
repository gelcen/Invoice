using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public interface IInvoiceRepository
    {
        Task<List<CoreBusiness.Invoice>> GetAll();
    }
}