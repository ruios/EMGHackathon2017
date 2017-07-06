using Nest;

namespace EmgAlexaHandler.Search.Documents
{
    public class CategoryNode
    {
        [Text(Name = "name")]
        public string Name { get; set; }
    }
}