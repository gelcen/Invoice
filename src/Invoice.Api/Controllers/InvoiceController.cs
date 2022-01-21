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

        public InvoiceController(IGetInvoicesUseCase getInvoicesUseCase,
            IGetInvoiceByIdUseCase getInvoiceUseCase)
        {
            this._getInvoicesUseCase = getInvoicesUseCase;
            this._getInvoiceByIdUseCase = getInvoiceUseCase;
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
    }
}
