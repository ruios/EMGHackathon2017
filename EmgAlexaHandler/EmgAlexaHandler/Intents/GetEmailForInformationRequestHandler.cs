using System;
using System.Collections.Generic;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace EmgAlexaHandler.Intents
{
    public class GetEmailForInformationRequestHandler : IIntentHandler
    {
        public bool CanHandle(string name)
        {
            return name == "GetEmailForInformationRequest";
        }

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            object education;
            if (session.Attributes["Education"] == null)
            {
                // TODO - ERROR RESPONSE, WE HAVE NO EDUCATION
            }
            education = session.Attributes["Education"]; // TODO - CAST TO TYPE

            var email = intentRequest.Intent.Slots["Email"].Value;

            var responseText = $"Before we make the information request, we also need your first name";

            var innerResponse = new PlainTextOutputSpeech
            {
                Text = responseText
            };

            var attr = new Dictionary<string, object>()
            {
                { "Education", education},
                { "Email", email }
            };

            return new HandlerResult() { Response = innerResponse, ResponseSessionAttributes = attr };
        }
    }
}