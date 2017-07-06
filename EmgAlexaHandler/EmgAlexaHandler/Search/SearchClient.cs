using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.Core;
using Elasticsearch.Net;
using Elasticsearch.Net.Aws;
using EmgAlexaHandler.Search.Documents;
using Nest;

namespace EmgAlexaHandler.Search
{
    public interface ISearchClient
    {
        SearchResult Search(string q);
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

        public SearchResult Search(string q)
        {
            LambdaLogger.Log($"Keyword: {q}");

            var query = Query<Education>.QueryString(e => e.Query(q).DefaultOperator(Operator.And));
            var request = CreateRequest(query);

            //var stream = new System.IO.MemoryStream();
            //_client.Serializer.Serialize(request, stream);
            //var jsonQuery = System.Text.Encoding.UTF8.GetString(stream.ToArray());
            //LambdaLogger.Log($"Query: {jsonQuery}");

            var response = _client.Search<Education>(request);
            return new SearchResult
            {
                Total = response.Total,
                Items = response.Documents.ToList()
            };
        }

        private ISearchRequest CreateRequest(QueryContainer query)
        {
            ISearchRequest request = new SearchRequest("index-0132", "education")
            {
                Query = query,
                Source = new Union<bool, ISourceFilter>(new SourceFilter
                {
                    Includes = new string[]
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
