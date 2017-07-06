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
            string responseText;
            var innerResponse = new PlainTextOutputSpeech();



            object education;
            if (session.Attributes["Education"] == null)
            {
                responseText = $"Yeah, no. Try again, human. Search for something.";

                innerResponse.Text = responseText;

                return new HandlerResult() { Response = innerResponse };
            }
            education = session.Attributes["Education"]; // TODO - CAST TO TYPE

            responseText = $"Fun facts about the education"; // TODO - GET FACTS FROM EDUCATION

            innerResponse.Text = responseText;

            var attr = new Dictionary<string, object>()
            {
                { "Education", education}
            };

            return new HandlerResult(){Response = innerResponse, ResponseSessionAttributes = attr};
        }
    }
}