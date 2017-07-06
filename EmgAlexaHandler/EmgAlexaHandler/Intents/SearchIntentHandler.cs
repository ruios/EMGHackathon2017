using System.Collections.Generic;
using Alexa.NET.Request.Type;
using EmgAlexaHandler.Search;
using EmgAlexaHandler.Search.Documents;
using EmgAlexaHandler.Search.Parameters;

namespace EmgAlexaHandler.Intents
{
    public class SearchIntentHandler : SearchHandlerBase
    {
        public override bool CanHandle(string name)
        {
            return name == "SearchEducation";
        }

        public override SearchResult Search(IntentRequest intentRequest)
        {
            var keyword = intentRequest.Intent.Slots["Education"].Value;
            var city = intentRequest.Intent.Slots["City"].Value;

            var client = new SearchClient();

            var placeId = client.GetKey(city, AttributeType.Place);

            var freetextParameter = new FreetextParameter(keyword);
            var placeParameter = new PlaceParameter(placeId);

            return client.Search(new List<ISearchParamter> { freetextParameter, placeParameter });
        }
    }
}