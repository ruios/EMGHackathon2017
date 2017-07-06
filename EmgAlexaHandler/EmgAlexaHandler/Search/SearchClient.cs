using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.Core;
using Elasticsearch.Net;
using Elasticsearch.Net.Aws;
using EmgAlexaHandler.Search.Documents;
using EmgAlexaHandler.Search.Parameters;
using Nest;

namespace EmgAlexaHandler.Search
{
    public interface ISearchClient
    {
        int GetKey(string q, AttributeType type);
        SearchResult Search(IReadOnlyList<ISearchParamter> parameters);
    }

    public class SearchResult
    {
        public long Total { get; set; }
        public IReadOnlyList<Education> Items { get; set; }
    }

    public class SearchClient : ISearchClient
    {
        private readonly IElasticClient _client;

        public SearchClient()
        {
            var cred = new AwsCredentials
            {
                AccessKey = Environment.GetEnvironmentVariable("AccessKey"),
                SecretKey = Environment.GetEnvironmentVariable("SecretKey"),
            };
            var awsConnection = new AwsHttpConnection("eu-west-1", new StaticCredentialsProvider(cred));
            var settings = new ConnectionSettings(new StaticConnectionPool(new[] { new Uri(Environment.GetEnvironmentVariable("Endpoint")) }), awsConnection, new SerializerFactory());
            _client = new ElasticClient(settings);
        }

        public int GetKey(string q, AttributeType type)
        {
            var query = new BoolQuery
            {
                Must = new[]
                {
                    Query<AttributeNode>.Term(i => i.Field("search-name").Value(q.ToLower())),
                    Query<AttributeNode>.Term(i => i.Field("type").Value(type)),
                }
            };

            var request = new SearchRequest("index-0012", "attribute")
            {
                Query = query,
                Source = new Union<bool, ISourceFilter>(new SourceFilter
                {
                    Includes = new[]
                    {
                        "id"
                    }
                }),
                Size = 1
            };

            var stream = new System.IO.MemoryStream();
            _client.Serializer.Serialize(request, stream);
            var jsonQuery = System.Text.Encoding.UTF8.GetString(stream.ToArray());
            LambdaLogger.Log($"Query: {jsonQuery}");

            var response = _client.Search<AttributeNode>(request);

            var attribute = response.Documents.FirstOrDefault();

            if (attribute == null)
                return 0;

            return attribute.Id;
        }

        public SearchResult Search(IReadOnlyList<ISearchParamter> parameters)
        {
            var must = new List<QueryContainer>();
            var mustNot = new List<QueryContainer>();
            var should = new List<QueryContainer>();

            foreach (var parameter in parameters)
            {
                switch (parameter.QueryType)
                {
                    case QueryType.Must:
                        must.Add(parameter.Query);
                        break;
                    case QueryType.MustNot:
                        mustNot.Add(parameter.Query);
                        break;
                    case QueryType.Should:
                        should.Add(parameter.Query);
                        break;
                }
            }

            var query = new BoolQuery
            {
                Must = must,
                MustNot = mustNot,
                Should = should
            };

            var request = CreateRequest(query);

            var stream = new System.IO.MemoryStream();
            _client.Serializer.Serialize(request, stream);
            var jsonQuery = System.Text.Encoding.UTF8.GetString(stream.ToArray());
            LambdaLogger.Log($"Query: {jsonQuery}");

            var response = _client.Search<Education>(request);
            return new SearchResult
            {
                Total = response.Total,
                Items = response.Documents.ToList()
            };
        }

        private ISearchRequest CreateRequest(QueryContainer query)
        {
            ISearchRequest request = new SearchRequest("index-0012", "education")
            {
                Query = query,
                Source = new Union<bool, ISourceFilter>(new SourceFilter
                {
                    Includes = new []
                    {
                        "id",
                        "name",
                        "institutes.name",
                        "categories.name",
                        "events.start.convertedStartDate",
                        "events.location.name",
                        "events.isDistance",
                        "fields.reviewAverage",
                        "fields.reviewCount",
                    }
                }),
                Size = 3
            };

            return request;
        }
    }
}
