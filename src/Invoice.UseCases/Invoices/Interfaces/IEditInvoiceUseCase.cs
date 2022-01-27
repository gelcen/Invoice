using Invoice.UseCases.Invoices.InputDtos;
using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public interface IEditInvoiceUseCase
    {
        Task Execute(EditInvoiceDto dto);
    }
}