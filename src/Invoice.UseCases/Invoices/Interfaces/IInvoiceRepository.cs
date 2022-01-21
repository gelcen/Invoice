using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public interface IInvoiceRepository
    {
        Task<List<CoreBusiness.Invoice>> GetAll();
        Task<CoreBusiness.Invoice> GetById(int invoiceId);
        Task AddInvoice(CoreBusiness.Invoice invoice);
        Task UpdateInvoice(CoreBusiness.Invoice invoice);
    }
}