using System.Collections.Generic;
using Nest;

namespace EmgAlexaHandler.Search.Documents
{
    [ElasticsearchType(IdProperty = nameof(Id), Name = "education")]
    public class Education
    {
        [Number(Name = "id")]
        public int Id { get; set; }

        [Text(Name = "name")]
        public string Name { get; set; }

        [Nested(Name = "institutes")]
        public IReadOnlyList<Institute> Institutes { get; set; }

        [Nested(Name = "events")]
        public IReadOnlyList<EventNode> Events { get; set; }

        [Nested(Name = "fields")]
        public FieldNode Fields { get; set; }

        [Nested(Name = "categories")]
        public IReadOnlyList<CategoryNode> Categories { get; set; }
    }
}
