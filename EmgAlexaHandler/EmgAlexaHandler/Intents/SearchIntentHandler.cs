using System.Collections.Generic;
using System.Linq;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using EmgAlexaHandler.Search;

namespace EmgAlexaHandler.Intents
{
    public class SearchIntentHandler : IIntentHandler
    {
        public bool CanHandle(string name)
        {
            return name == "SearchIntent";
        }

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            string keyword = intentRequest.Intent.Slots["Keyword"].Value;

            var client = new SearchClient();
            var result = client.Search(keyword);

            if (!result.Any())
                return new HandlerResult()
                {
                    Response = new PlainTextOutputSpeech
                    {
                        Text = "Your search word sucked. Try again."
                    }
                };

            var responseText = $"We found some results. Here are the top three: {string.Join(", ", result.Select(i => i.Name))}. Are you happy with these results, or do you want to do a new search??";

            var innerResponse = new PlainTextOutputSpeech
            {
                Text = responseText
            };

            
            return new HandlerResult(){Response = innerResponse, ResponseSessionAttributes = new Dictionary<string, object>()};
        }
    }
}