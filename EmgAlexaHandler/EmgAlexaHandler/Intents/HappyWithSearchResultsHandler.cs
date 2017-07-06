using System.Collections.Generic;
using System.Linq;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using EmgAlexaHandler.Search.Documents;

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
            if (session.Attributes["EducationList"] == null)
            {
                var errorResponse = new PlainTextOutputSpeech
                {
                    Text = "Something weird happened. Let's try another search."
                };

                return new HandlerResult {Response = errorResponse};
            }
            var educationList = (Education[])session.Attributes["EducationList"];
            
            var selectedResult = string.Join(", ", educationList.Select(i => $"{i.Name} from {i.Institutes.First().Name}"));
            var responseText = $"Here are the three educations: {selectedResult}. Which one would you like to know more about?";

            var innerResponse = new PlainTextOutputSpeech
            {
                Text = responseText
            };

            var attr = new Dictionary<string, object>()
            {
                { "EducationList", educationList}
            };

            return new HandlerResult() { Response = innerResponse, ResponseSessionAttributes = attr};
        }
    }
}