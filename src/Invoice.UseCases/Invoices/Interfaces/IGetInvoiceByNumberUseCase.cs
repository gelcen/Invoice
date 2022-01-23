using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public interface IGetInvoiceByNumberUseCase
    {
        Task<CoreBusiness.Invoice> Execute(int invoiceId);
    }
}