using CsvHelper;
using CsvHelper.Configuration;
using Invoice.UseCases.Invoices;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Invoice.Plugins.Repository.Csv.Invoices
{
    public class InvoiceCsvRepository : IInvoiceRepository
    {
        private readonly InvoiceCsvOptions _options;
        private readonly CsvConfiguration _csvConfig = new(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = false,
        };
        private readonly CultureInfo _provider = new("en-US");

        public InvoiceCsvRepository(IOptions<InvoiceCsvOptions> options)
        {
            this._options = options.Value;
        }

        public async Task AddInvoice(CoreBusiness.Invoice invoice)
        {
            using var streamReader = File.OpenText(_options.PathToCsvFile);
            using var csvReader = new CsvReader(streamReader, _csvConfig);

            List<InvoiceRecord> invoiceRecords = new();

            await foreach (var invoiceRecord in csvReader.GetRecordsAsync<InvoiceRecord>())
            {
                invoiceRecords.Add(invoiceRecord);
            }
            streamReader.Close();

            using var stream = File.Open(_options.PathToCsvFile, FileMode.Open);
            using var streamWriter = new StreamWriter(stream);
            using var csvWriter = new CsvWriter(streamWriter, _csvConfig);

            invoiceRecords.Add(CreateRecordFromInvoice(invoice));

            await csvWriter.WriteRecordsAsync<InvoiceRecord>(invoiceRecords);

            csvWriter.Flush();
        }

        public IEnumerable<CoreBusiness.Invoice> GetAll()
        {
            using var streamReader = File.OpenText(_options.PathToCsvFile);
            using var csvReader = new CsvReader(streamReader, _csvConfig);

            var invoices = csvReader.GetRecords<InvoiceRecord>();

            var invoiceList = new List<CoreBusiness.Invoice>();


            foreach (var invoiceRecord in invoices)
            {
                yield return CreateInvoiceFromRecord(invoiceRecord);
            }
        }

        public async Task<CoreBusiness.Invoice> GetByNumber(int number)
        {
            CoreBusiness.Invoice invoice = null;

            using var streamReader = File.OpenText(_options.PathToCsvFile);
            using var csvReader = new CsvReader(streamReader, _csvConfig);

            await foreach (var invoiceRecord in csvReader.GetRecordsAsync<InvoiceRecord>())
            {
                if (invoiceRecord.Number == number)
                {
                    invoice = CreateInvoiceFromRecord(invoiceRecord);
                    break;
                }
            }

            return invoice;
        }

        public async Task UpdateInvoice(CoreBusiness.Invoice invoice, int? previousNumber)
        {
            using var streamReader = File.OpenText(_options.PathToCsvFile);
            using var csvReader = new CsvReader(streamReader, _csvConfig);

            List<InvoiceRecord> invoiceRecords = new();

            await foreach (var invoiceRecord in csvReader.GetRecordsAsync<InvoiceRecord>())
            {
                if (invoiceRecord.Number == invoice.Number)
                {
                    invoiceRecord.Number = invoice.Number;
                    invoiceRecord.Amount = invoice.Amount.ToString();
                    invoiceRecord.PaymentMethod = (int)invoice.PaymentMethod;
                    invoiceRecord.ModifiedAt = invoice.ModifiedAt;
                }
                else if (previousNumber.HasValue && invoiceRecord.Number == previousNumber)
                {
                    invoiceRecord.Number = invoice.Number;
                    invoiceRecord.Amount = invoice.Amount.ToString();
                    invoiceRecord.PaymentMethod = (int)invoice.PaymentMethod;
                    invoiceRecord.ModifiedAt = invoice.ModifiedAt;
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
            if (record.Amount.Contains(',')) 
                record.Amount = record.Amount.Replace(',', '.');

            return new CoreBusiness.Invoice()
            {
                Number = record.Number,
                CreatedAt = record.CreatedAt,
                ProcessingStatus = (CoreBusiness.ProcessingStatus)record.ProcessingStatus,
                Amount = float.Parse(record.Amount, NumberStyles.Float, _provider),
                PaymentMethod = (CoreBusiness.PaymentMethod)record.PaymentMethod,
                ModifiedAt = record.ModifiedAt
            };
        }

        private InvoiceRecord CreateRecordFromInvoice(CoreBusiness.Invoice invoice)
        {
            return new InvoiceRecord()
            {
                Number = invoice.Number,
                CreatedAt = invoice.CreatedAt,
                ProcessingStatus = (int)invoice.ProcessingStatus,
                Amount = invoice.Amount.ToString(),
                PaymentMethod = (int)invoice.PaymentMethod,
                ModifiedAt = invoice.ModifiedAt
            };
        }
    }
}
