using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public interface IGetInvoiceByIdUseCase
    {
        Task<CoreBusiness.Invoice> Execute(int invoiceId);
    }
}