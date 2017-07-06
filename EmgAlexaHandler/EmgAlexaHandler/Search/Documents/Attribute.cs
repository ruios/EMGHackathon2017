using Nest;

namespace EmgAlexaHandler.Search.Documents
{
    public enum AttributeType
    {
        Institute = 1,
        Category = 2,
        EducationType = 3,
        EventType = 4,
        Place = 5,
        Certificate = 6,
        Keyword = 7,
        CertificateCategory = 8,
        Language = 9
    }

    [ElasticsearchType(Name = "attribute", IdProperty = "UniqueId")]
    public class AttributeNode
    {
        [Number(Name = "id")]
        public int Id { get; set; }
    }
}
