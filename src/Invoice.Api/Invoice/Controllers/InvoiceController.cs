using Invoice.Api.Invoice.Requests;
using Invoice.UseCases.Invoices;
using Invoice.UseCases.Shared.QueryProcessor;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using System.Threading.Tasks;

namespace Invoice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IGetInvoicesUseCase _getInvoicesUseCase;
        private readonly IGetInvoiceByNumberUseCase _getInvoiceByNumberUseCase;
        private readonly IAddInvoiceUseCase _addInvoiceUseCase;
        private readonly IEditInvoiceUseCase _editInvoiceUseCase;

        public InvoiceController(IGetInvoicesUseCase getInvoicesUseCase,
            IGetInvoiceByNumberUseCase getInvoiceUseCase,
            IAddInvoiceUseCase addInvoiceUseCase,
            IEditInvoiceUseCase editInvoiceUseCase)
        {
            this._getInvoicesUseCase = getInvoicesUseCase;
            this._getInvoiceByNumberUseCase = getInvoiceUseCase;
            this._addInvoiceUseCase = addInvoiceUseCase;
            this._editInvoiceUseCase = editInvoiceUseCase;
        }

        [HttpGet] 
        public IActionResult GetInvoices(string filters, string sorts, int page, int pageSize)
        {
            var queryModel = new QueryModel()
            {
                Filters = filters,
                Sorts = sorts,
                Page = page,
                PageSize = pageSize
            };
            var result = _getInvoicesUseCase.Execute(queryModel);
            return Ok(result);
        }

        [HttpGet("{number:int}")]
        public async Task<IActionResult> GetInvoiceByNumber(int number)
        {
            var result = await _getInvoiceByNumberUseCase.Execute(number);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoice([FromBody] AddInvoiceRequest request)
        {
            var createdInvoice = await _addInvoiceUseCase.Execute(
                new UseCases.Invoices.InputDtos.AddInvoiceDto()
                {
                    Number = request.Number,
                    Amount = request.Amount,
                    PaymentMethod = request.PaymentMethod
                });
            return CreatedAtAction(nameof(AddInvoice), new { id = createdInvoice.Number }, createdInvoice);
        }

        [HttpPut]
        public async Task<IActionResult> EditInvoice([FromBody] UpdateInvoiceRequest request)
        {
            await _editInvoiceUseCase.Execute(new UseCases.Invoices.InputDtos.EditInvoiceDto()
            {
                Number = request.Number,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod
            });
            return NoContent();
        }
    }
}
