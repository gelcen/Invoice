using Invoice.UseCases.Invoices;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetInvoices()
        {
            var result = await _getInvoicesUseCase.Execute();
            return Ok(result);
        }

        [HttpGet("{number:int}")]
        public async Task<IActionResult> GetInvoiceByNumber(int number)
        {
            var result = await _getInvoiceByNumberUseCase.Execute(number);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoice(int? number, float? amount)
        {
            var createdInvoice = await _addInvoiceUseCase.Execute(number, amount);
            return CreatedAtAction(nameof(GetInvoiceByNumber), new { id = createdInvoice.Number }, createdInvoice);
        }

        [HttpPut]
        public async Task<IActionResult> EditInvoice(int? number, float? amount)
        {
            await _editInvoiceUseCase.Execute(number, amount);
            return NoContent();
        }
    }
}
