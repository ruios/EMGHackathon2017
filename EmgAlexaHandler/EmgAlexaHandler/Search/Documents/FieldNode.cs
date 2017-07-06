using Nest;

namespace EmgAlexaHandler.Search.Documents
{
    public class FieldNode
    {
        [Number(Name = "reviewAverage")]
        public decimal? ReviewAverage { get; set; }

        [Number(Name = "reviewCount")]
        public int? ReviewCount { get; set; }
    }
}