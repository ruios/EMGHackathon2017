using Nest;

namespace EmgAlexaHandler.Search.Documents
{
    public class EventNode
    {
        [Nested(Name = "start")]
        public StartInfoNode Start { get; set; }

        [Nested(Name = "location")]
        public LocationNode Location { get; set; }
    }
}