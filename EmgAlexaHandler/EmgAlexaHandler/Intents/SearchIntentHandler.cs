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
            return name == "SearchEducation";
        }

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            var keyword = intentRequest.Intent.Slots["Education"].Value;
            var city = intentRequest.Intent.Slots["City"].Value;

            var client = new SearchClient();
            var result = client.Search(keyword);

            if (!result.Items.Any())
            {
                return new HandlerResult()
                {
                    Response = new PlainTextOutputSpeech
                    {
                        Text = "Your search word sucked. Try again."
                    }
                };
            }

            var selectedResult = string.Join(", ", result.Items.Select(i => $"{i.Name} from {i.Institutes.First().Name}"));
            var responseText = $"We found {result.Total} results. Here are the top three: {selectedResult}. Are you happy with these results, or do you want to do a new search??";
            
            var innerResponse = new PlainTextOutputSpeech
            {
                Text = responseText
            };

            var attr = new Dictionary<string, object>()
            {
                { "EducationList", result.Items.ToArray()}
            };

            return new HandlerResult()
            {
                Response = innerResponse, ResponseSessionAttributes = attr
            };
        }
    }
}