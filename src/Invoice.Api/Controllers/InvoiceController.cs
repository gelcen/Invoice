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

        public InvoiceController(IGetInvoicesUseCase getInvoicesUseCase)
        {
            _getInvoicesUseCase = getInvoicesUseCase;
        }

        [HttpGet] 
        public async Task<IActionResult> GetInvoices()
        {
            var result = await _getInvoicesUseCase.Execute();
            return Ok(result);
        }
    }
}
