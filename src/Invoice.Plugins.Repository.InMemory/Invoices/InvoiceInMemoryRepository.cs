using Invoice.UseCases.Invoices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice.Plugins.Repository.InMemory.Invoices
{
    public class InvoiceInMemoryRepository //: IInvoiceRepository
    {
        private static List<CoreBusiness.Invoice> _invoices;

        public InvoiceInMemoryRepository()
        {
            #region Create fake data
            _invoices = new List<CoreBusiness.Invoice>()
            {
                new CoreBusiness.Invoice
                {
                    Number = 1,
                    CreatedAt = DateTime.Parse("11.03.2020  8:11:20"),
                    ProcessingStatus = CoreBusiness.ProcessingStatus.Canceled,
                    Amount = 10.0f,
                    PaymentMethod = CoreBusiness.PaymentMethod.CreditCard
                },
                new CoreBusiness.Invoice
                {
                    Number = 2,
                    CreatedAt = DateTime.Parse("12.03.2020  8:04:12"),
                    ProcessingStatus = CoreBusiness.ProcessingStatus.Paid,
                    Amount = 120.0f,
                    PaymentMethod = CoreBusiness.PaymentMethod.ElectronicCheck
                },
                new CoreBusiness.Invoice
                {
                    Number = 3,
                    CreatedAt = DateTime.Parse("11.03.2020  22:01:39"),
                    ProcessingStatus = CoreBusiness.ProcessingStatus.Canceled,
                    Amount = 30.0f,
                    PaymentMethod = CoreBusiness.PaymentMethod.DebitCard
                },
                new CoreBusiness.Invoice
                {
                    Number = 4,
                    CreatedAt = DateTime.Parse("08.04.2020  12:37:08"),
                    ProcessingStatus = CoreBusiness.ProcessingStatus.New,
                    Amount = 1.0f,
                    PaymentMethod = CoreBusiness.PaymentMethod.ElectronicCheck
                },
                new CoreBusiness.Invoice
                {
                    Number = 5,
                    CreatedAt = DateTime.Parse("16.03.2020  10:41:39"),
                    ProcessingStatus = CoreBusiness.ProcessingStatus.New,
                    Amount = 10.12f,
                    PaymentMethod = CoreBusiness.PaymentMethod.ElectronicCheck
                },
                new CoreBusiness.Invoice
                {
                    Number = 6,
                    CreatedAt = DateTime.Parse("14.05.2020  10:59:17"),
                    ProcessingStatus = CoreBusiness.ProcessingStatus.Paid,
                    Amount = 5.0f,
                    PaymentMethod = CoreBusiness.PaymentMethod.CreditCard
                },
                new CoreBusiness.Invoice
                {
                    Number = 7,
                    CreatedAt = DateTime.Parse("09.04.2020  18:20:32"),
                    ProcessingStatus = CoreBusiness.ProcessingStatus.Canceled,
                    Amount = 1230.0f,
                    PaymentMethod = CoreBusiness.PaymentMethod.CreditCard
                },
                new CoreBusiness.Invoice
                {
                    Number = 8,
                    CreatedAt = DateTime.Parse("16.03.2020  11:13:38"),
                    ProcessingStatus = CoreBusiness.ProcessingStatus.Paid,
                    Amount = 132210.0f,
                    PaymentMethod = CoreBusiness.PaymentMethod.DebitCard
                },
                new CoreBusiness.Invoice
                {
                    Number = 9,
                    CreatedAt = DateTime.Parse("13.04.2020  11:37:09"),
                    ProcessingStatus = CoreBusiness.ProcessingStatus.Canceled,
                    Amount = 10.0f,
                    PaymentMethod = CoreBusiness.PaymentMethod.CreditCard
                },
                new CoreBusiness.Invoice
                {
                    Number = 10,
                    CreatedAt = DateTime.Parse("14.05.2020  10:53:19"),
                    ProcessingStatus = CoreBusiness.ProcessingStatus.New,
                    Amount = 133.0f,
                    PaymentMethod = CoreBusiness.PaymentMethod.ElectronicCheck
                },
            };
            #endregion
        }

        public Task AddInvoice(CoreBusiness.Invoice invoice)
        {
            _invoices.Add(invoice);
            return Task.CompletedTask;
        }

        public Task<List<CoreBusiness.Invoice>> GetAll()
        {
            return Task.FromResult(_invoices);
        }

        public Task<CoreBusiness.Invoice> GetByNumber(int invoiceId)
        {
            return Task.FromResult(_invoices.Find(x => x.Number == invoiceId));
        }

        public async Task UpdateInvoice(CoreBusiness.Invoice invoice)
        {
            var changedInvoice= await GetByNumber(invoice.Number);
            changedInvoice.Amount = invoice.Amount;
        }
    }
}
