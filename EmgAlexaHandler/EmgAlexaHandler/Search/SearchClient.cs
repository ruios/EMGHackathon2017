using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.Aws;
using EmgAlexaHandler.Search.Documents;
using Nest;

namespace EmgAlexaHandler.Search
{
    public interface ISearchClient
    {
        IReadOnlyList<Education> Search(string q);
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
            settings.DisableDirectStreaming();
            _client = new ElasticClient(settings);
        }

        public IReadOnlyList<Education> Search(string q)
        {
            var query = Query<Education>.QueryString(e => e.Query(q).DefaultOperator(Operator.And));
            var request = CreateRequest(query);
            var response = _client.Search<Education>(request);
            return response.Documents.ToList();
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
                        "institutes.id",
                        "institutes.name",
                    }
                }),
                Size = 3,
            };

            return request;
        }
    }
}
