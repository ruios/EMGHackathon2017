using Nest;

namespace EmgAlexaHandler.Search.Documents
{
    public class Institute
    {
        [Number(Name = "id")]
        public int Id { get; set; }
        [Text(Name = "name")]
        public string Name { get; set; }
    }
}