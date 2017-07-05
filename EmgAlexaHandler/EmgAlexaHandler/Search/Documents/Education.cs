using System.Collections.Generic;
using Nest;

namespace EmgAlexaHandler.Search.Documents
{
    [ElasticsearchType(IdProperty = nameof(Id), Name = "education")]
    public class Education
    {
        [Number(Name = "id")]
        public int Id { get; set; }
        [Nested(Name = "institutes")]
        public List<Institute> Institutes { get; set; }
        [Text(Name = "name")]
        public string Name { get; set; }
    }
}
