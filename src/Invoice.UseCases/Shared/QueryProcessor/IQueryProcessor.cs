using System.Linq;

namespace Invoice.UseCases.Shared.QueryProcessor
{
    public interface IQueryProcessor<T> where T: class
    {
        (IQueryable<T>, int) Apply(QueryModel model, IQueryable<T> source);
    }
}
