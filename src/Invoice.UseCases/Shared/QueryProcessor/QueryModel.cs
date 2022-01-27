namespace Invoice.UseCases.Shared.QueryProcessor
{
    public class QueryModel
    {
        public string Filters { get; set; }
        public string Sorts { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
