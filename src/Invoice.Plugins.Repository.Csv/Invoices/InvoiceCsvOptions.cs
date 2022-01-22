using System;

namespace Invoice.Plugins.Repository.Csv.Invoices
{
    public class InvoiceCsvOptions
    {
        public const string CsvDataSource = "CsvDataSource";

        public string PathToCsvFile { get; set; } = String.Empty;
    }
}
