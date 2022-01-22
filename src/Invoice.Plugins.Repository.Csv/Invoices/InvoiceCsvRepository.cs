using CsvHelper;
using CsvHelper.Configuration;
using Invoice.UseCases.Invoices;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Plugins.Repository.Csv.Invoices
{
    public class InvoiceCsvRepository : IInvoiceRepository
    {
        private readonly InvoiceCsvOptions _options;
        private readonly CsvConfiguration _csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = false
        };

        public InvoiceCsvRepository(IOptions<InvoiceCsvOptions> options)
        {
            this._options = options.Value;
        }

        public Task AddInvoice(CoreBusiness.Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CoreBusiness.Invoice>> GetAll()
        {
            string filename = _options.PathToCsvFile;
            using var streamReader = File.OpenText(filename);
            using var csvReader = new CsvReader(streamReader, _csvConfig);

            var invoices = csvReader.GetRecordsAsync<InvoiceRecord>();

            var invoiceList = new List<CoreBusiness.Invoice>();

            var provider = new CultureInfo("en-US");

            await foreach (var invoiceRecord in invoices)
            {
                invoiceList.Add(new CoreBusiness.Invoice()
                {
                    InvoiceId = invoiceRecord.Id,
                    CreatedAt = invoiceRecord.Created,
                    ProcessingStatus = (CoreBusiness.ProcessingStatus)invoiceRecord.ProcessingStatus,
                    Amount = float.Parse(invoiceRecord.Amount, NumberStyles.Float, provider),
                    PaymentMethod = (CoreBusiness.PaymentMethod)invoiceRecord.PaymentMethod
                });
            }

            return invoiceList;
        }

        public Task<CoreBusiness.Invoice> GetById(int invoiceId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateInvoice(CoreBusiness.Invoice invoice)
        {
            throw new NotImplementedException();
        }
    }
}
