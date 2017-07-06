using EmgAlexaHandler.Search.Documents;
using Nest;

namespace EmgAlexaHandler.Search.Parameters
{
    public interface ISearchParamter
    {
        QueryType QueryType { get; }
        QueryContainer Query { get; }
    }

    public enum QueryType
    {
        Must,
        MustNot,
        Should
    }

    public class FreetextParameter : ISearchParamter
    {
        public string Keyword { get; }
        public QueryType QueryType => QueryType.Should;
        public QueryContainer Query => Query<Education>.QueryString(e => e.Query(Keyword).DefaultOperator(Operator.And));

        public FreetextParameter(string keyword)
        {
            Keyword = keyword;
        }
    }

    public class PlaceParameter : ISearchParamter
    {
        public int Id { get; }
        public QueryType QueryType => QueryType.Must;
        public QueryContainer Query => Query<Education>.Nested(n => n.Path("events").Query(q => q.Term(e => e.Field("events.places.id").Value(Id))));

        public PlaceParameter(int id)
        {
            Id = id;
        }
    }
}
