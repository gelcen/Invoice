using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public interface IAddInvoiceUseCase
    {
        Task<CoreBusiness.Invoice> Execute(int? invoiceId, float? amount);
    }
}