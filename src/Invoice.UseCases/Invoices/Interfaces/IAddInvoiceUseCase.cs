using Invoice.UseCases.Invoices.InputDtos;
using System.Threading.Tasks;

namespace Invoice.UseCases.Invoices
{
    public interface IAddInvoiceUseCase
    {
        Task<CoreBusiness.Invoice> Execute(AddInvoiceDto dto);
    }
}