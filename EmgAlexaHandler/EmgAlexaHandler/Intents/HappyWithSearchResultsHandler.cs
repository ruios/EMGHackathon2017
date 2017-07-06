using System.Collections.Generic;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

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
                // TODO - ERROR, WE HAVE NO SEARCH RESULT
            }
            var educationList = session.Attributes["EducationList"]; // TODO - CAST TO LIST


            var responseText = $"Here are the three educations, which one would you like to know more about?";

            // TODO - LIST THE EDUCATIONS IN THE RESPONSE

            var innerResponse = new PlainTextOutputSpeech
            {
                Text = responseText
            };

            var attr = new Dictionary<string, object>()
            {
                { "EducationList", educationList}
            };

            return new HandlerResult() { Response = innerResponse };
        }
    }
}