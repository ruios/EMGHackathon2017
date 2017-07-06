using System;
using System.Collections.Generic;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using EmgAlexaHandler.Search.Documents;

namespace EmgAlexaHandler.Intents
{
    public class GetEmailForInformationRequestHandler : EducationIntentBase
    {
        public override bool CanHandle(string name)
        {
            return name == "GetEmailForInformationRequest";
        }

        public override HandlerResult GetResponse(Education education, IntentRequest intentRequest, Session session)
        {
            var email = intentRequest.Intent.Slots["Email"].Value;

            var responseText = $"Before we make the information request, we also need your first name";

            var innerResponse = new PlainTextOutputSpeech()
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