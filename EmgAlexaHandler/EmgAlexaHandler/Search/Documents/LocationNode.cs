using Nest;

namespace EmgAlexaHandler.Search.Documents
{
    public class LocationNode
    {
        [Text(Name = "name")]
        public string Name { get; set; }
    }
}