using Invoice.UseCases.Invoices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IGetInvoicesUseCase _getInvoicesUseCase;
        private readonly IGetInvoiceByIdUseCase _getInvoiceByIdUseCase;
        private readonly IAddInvoiceUseCase _addInvoiceUseCase;
        private readonly IEditInvoiceUseCase _editInvoiceUseCase;

        public InvoiceController(IGetInvoicesUseCase getInvoicesUseCase,
            IGetInvoiceByIdUseCase getInvoiceUseCase,
            IAddInvoiceUseCase addInvoiceUseCase,
            IEditInvoiceUseCase editInvoiceUseCase)
        {
            this._getInvoicesUseCase = getInvoicesUseCase;
            this._getInvoiceByIdUseCase = getInvoiceUseCase;
            this._addInvoiceUseCase = addInvoiceUseCase;
            this._editInvoiceUseCase = editInvoiceUseCase;
        }

        [HttpGet] 
        public async Task<IActionResult> GetInvoices()
        {
            var result = await _getInvoicesUseCase.Execute();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            var result = await _getInvoiceByIdUseCase.Execute(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoice(int? id, float? amount)
        {
            var createdInvoice = await _addInvoiceUseCase.Execute(id, amount);
            return CreatedAtAction(nameof(GetInvoiceById), new { id = createdInvoice.InvoiceId }, createdInvoice);
        }

        [HttpPut]
        public async Task<IActionResult> EditInvoice(int? id, float? amount)
        {
            await _editInvoiceUseCase.Execute(id, amount);
            return NoContent();
        }
    }
}
