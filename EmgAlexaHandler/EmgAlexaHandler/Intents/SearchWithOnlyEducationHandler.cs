using Alexa.NET.Request.Type;
using EmgAlexaHandler.Search;
using EmgAlexaHandler.Search.Parameters;

namespace EmgAlexaHandler.Intents
{
    public class SearchWithOnlyEducationHandler : SearchHandlerBase
    {
        public override bool CanHandle(string name)
        {
            return name == "SearchWithOnlyEducation";
        }

        public override SearchResult Search(IntentRequest intentRequest)
        {
            var keyword = intentRequest.Intent.Slots["Education"].Value;

            var parameter = new FreetextParameter(keyword);

            var client = new SearchClient();
            return client.Search(new[] { parameter });
        }
    }
}