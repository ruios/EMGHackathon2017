using System.Collections.Generic;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace EmgAlexaHandler.Intents
{
    public class GetMoreInfoAboutEducationHandler : IIntentHandler
    {
        public bool CanHandle(string name)
        {
            return name == "GetMoreInfoAboutEducation";
        }

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            object education;
            if (session.Attributes["Education"] == null)
            {
                // TODO - ERROR RESPONSE, WE HAVE NO EDUCATION
            }
            education = session.Attributes["Education"]; // TODO - CAST TO TYPE

            var responseText = $"Fun facts about the education"; // TODO - GET FACTS FROM EDUCATION

            var innerResponse = new PlainTextOutputSpeech
            {
                Text = responseText
            };

            var attr = new Dictionary<string, object>()
            {
                { "Education", education}
            };

            return new HandlerResult(){Response = innerResponse, ResponseSessionAttributes = attr};
        }
    }
}