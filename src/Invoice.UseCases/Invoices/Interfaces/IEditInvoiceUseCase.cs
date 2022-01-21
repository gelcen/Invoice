using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public interface IEditInvoiceUseCase
    {
        Task Execute(int? invoiceId, float? amount);
    }
}