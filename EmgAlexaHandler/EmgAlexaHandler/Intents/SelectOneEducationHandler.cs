using System.Collections.Generic;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace EmgAlexaHandler.Intents
{
    public class SelectOneEducationHandler : IIntentHandler
    {
        public bool CanHandle(string name)
        {
            return name == "SelectOneEducation";
        }

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            if (session.Attributes["EducationList"] == null)
            {
                // TODO - ERROR, WE HAVE NO SEARCH RESULT
            }
            var educationList = session.Attributes["EducationList"]; // TODO - CAST TO LIST

            var selected = (string)intentRequest.Intent.Slots["EducationNumber"].Value.ToLower();


            // TODO - SELECT CORRECT EDU
            object education = null;
            if (selected.Contains("third") || selected.Contains("3") || selected.Contains("three"))
            {
                
            }

            else if (selected.Contains("second") || selected.Contains("2") || selected.Contains("two"))
            {

            }

            else if (selected.Contains("first") || selected.Contains("1") || selected.Contains("one"))
            {

            }

            // TODO - response text
            var responseText = $"Some info about education... Do you want to make an information request or hear more about the education?";
            
            var innerResponse = new PlainTextOutputSpeech
            {
                Text = responseText
            };

            var attr = new Dictionary<string, object>()
            {
                { "EducationList", educationList},
                {"Education", education }
            };

            return new HandlerResult() { Response = innerResponse, ResponseSessionAttributes = attr};
        }
    }
}