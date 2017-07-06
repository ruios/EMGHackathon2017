using System.Collections.Generic;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace EmgAlexaHandler.Intents
{
    public class StartInformationRequestHandler : IIntentHandler
    {
        public bool CanHandle(string name)
        {
            return name == "StartInformationRequest";
        }

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            string responseText;
            var innerResponse = new PlainTextOutputSpeech();

            object education;
            if (session.Attributes["Education"] == null)
            {
                responseText = $"This didn't work like, at all. Try another search word.";

                innerResponse.Text = responseText;

                return new HandlerResult() { Response = innerResponse};
            }
            education = session.Attributes["Education"]; // TODO - CAST TO TYPE

            responseText = $"To make an information request, we will first need your email address.";

            innerResponse.Text = responseText;

            var attr = new Dictionary<string, object>()
            {
                { "Education", education}
            };

            return new HandlerResult() { Response = innerResponse, ResponseSessionAttributes = attr };
        }
    }
}