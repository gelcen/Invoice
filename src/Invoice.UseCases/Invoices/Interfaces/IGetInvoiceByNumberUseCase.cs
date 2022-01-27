using Invoice.UseCases.Invoices.ViewModels;
using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public interface IGetInvoiceByNumberUseCase
    {
        Task<GetInvoiceByNumberViewModel> Execute(int invoiceId);
    }
}