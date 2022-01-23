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
        private readonly CsvConfiguration _csvConfig = new(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = false
        };
        private readonly CultureInfo _provider = new("en-US");

        public InvoiceCsvRepository(IOptions<InvoiceCsvOptions> options)
        {
            this._options = options.Value;
        }

        public Task AddInvoice(CoreBusiness.Invoice invoice)
        {
            using var stream = File.Open(_options.PathToCsvFile, FileMode.Append);
            using var streamWriter = new StreamWriter(stream);
            using var csvWriter = new CsvWriter(streamWriter, _csvConfig);
            csvWriter.WriteRecord(CreateRecordFromInvoice(invoice));
            return Task.CompletedTask;
        }

        public async Task<List<CoreBusiness.Invoice>> GetAll()
        {
            using var streamReader = File.OpenText(_options.PathToCsvFile);
            using var csvReader = new CsvReader(streamReader, _csvConfig);

            var invoices = csvReader.GetRecordsAsync<InvoiceRecord>();

            var invoiceList = new List<CoreBusiness.Invoice>();


            await foreach (var invoiceRecord in invoices)
            {
                invoiceList.Add(CreateInvoiceFromRecord(invoiceRecord));
            }

            return invoiceList;
        }

        public async Task<CoreBusiness.Invoice> GetById(int invoiceId)
        {
            CoreBusiness.Invoice invoice = null;

            using var streamReader = File.OpenText(_options.PathToCsvFile);
            using var csvReader = new CsvReader(streamReader, _csvConfig);

            await foreach (var invoiceRecord in csvReader.GetRecordsAsync<InvoiceRecord>())
            {
                if (invoiceRecord.Id == invoiceId)
                {
                    invoice = CreateInvoiceFromRecord(invoiceRecord);
                    break;
                }
            }

            return invoice;
        }

        public async Task UpdateInvoice(CoreBusiness.Invoice invoice)
        {
            using var streamReader = File.OpenText(_options.PathToCsvFile);
            using var csvReader = new CsvReader(streamReader, _csvConfig);

            List<InvoiceRecord> invoiceRecords = new();

            await foreach (var invoiceRecord in csvReader.GetRecordsAsync<InvoiceRecord>())
            {
                if (invoiceRecord.Id == invoice.InvoiceId)
                {
                    invoiceRecord.Amount = invoice.Amount.ToString();
                }
                invoiceRecords.Add(invoiceRecord);
            }
            streamReader.Close();

            using var stream = File.Open(_options.PathToCsvFile, FileMode.Open);
            using var streamWriter = new StreamWriter(stream);
            using var csvWriter = new CsvWriter(streamWriter, _csvConfig);

            await csvWriter.WriteRecordsAsync<InvoiceRecord>(invoiceRecords);

            csvWriter.Flush();
        }

        private CoreBusiness.Invoice CreateInvoiceFromRecord(InvoiceRecord record)
        {
            return new CoreBusiness.Invoice()
            {
                InvoiceId = record.Id,
                CreatedAt = record.Created,
                ProcessingStatus = (CoreBusiness.ProcessingStatus)record.ProcessingStatus,
                Amount = float.Parse(record.Amount, NumberStyles.Float, _provider),
                PaymentMethod = (CoreBusiness.PaymentMethod)record.PaymentMethod
            };
        }

        private InvoiceRecord CreateRecordFromInvoice(CoreBusiness.Invoice invoice)
        {
            return new InvoiceRecord()
            {
                Id = invoice.InvoiceId,
                Created = invoice.CreatedAt,
                ProcessingStatus = (int)invoice.ProcessingStatus,
                Amount = invoice.Amount.ToString(),
                PaymentMethod = (int)invoice.PaymentMethod
            };
        }
    }
}
