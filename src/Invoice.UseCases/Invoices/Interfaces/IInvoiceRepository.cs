using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public interface IInvoiceRepository
    {
        IEnumerable<CoreBusiness.Invoice> GetAll();
        Task<CoreBusiness.Invoice> GetByNumber(int number);
        Task AddInvoice(CoreBusiness.Invoice invoice);
        Task UpdateInvoice(CoreBusiness.Invoice invoice);
    }
}