using System.Collections.Generic;
using System.Linq;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using EmgAlexaHandler.Search.Documents;
using Newtonsoft.Json.Linq;

namespace EmgAlexaHandler.Intents
{
    public class HappyWithSearchResultsHandler : IIntentHandler
    {
        public bool CanHandle(string name)
        {
            return name == "HappyWithSearchResults";
        }

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            var errorResponse = new HandlerResult
            {
                Response = new PlainTextOutputSpeech
                {
                    Text = "Something weird happened. Let's try another search."
                }
            };

            if (session.Attributes["EducationList"] == null)
            {
                return errorResponse;
            }

            var jArray = session.Attributes["EducationList"] as JArray;
            if (jArray != null)
            {
                var educationList = jArray.ToObject<List<Education>>();
            
                var selectedResult = string.Join(", ", educationList.Select(i => $"{i.Name} from {i.Institutes.First().Name}"));
                var responseText = $"Here are the three educations: {selectedResult}. Which one would you like to know more about?";

                var innerResponse = new PlainTextOutputSpeech
                {
                    Text = responseText
                };

                var attr = new Dictionary<string, object>()
                {
                    { "EducationList", educationList},
                    {"Previous", IntentTypes.Intent.HappyWithSearchResults},
                };

                return new HandlerResult() { Response = innerResponse, ResponseSessionAttributes = attr};
            }

            return errorResponse;
        }
    }
}