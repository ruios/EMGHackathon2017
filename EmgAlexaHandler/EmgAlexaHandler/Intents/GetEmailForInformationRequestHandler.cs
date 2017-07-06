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
            var email = (string)intentRequest.Intent.Slots["Email"].Value;

            if (string.IsNullOrEmpty(email))
            {
                var text = $"To continue with the information request, you need to first give me your email address. Pronounce it clearly.";

                var resp = new PlainTextOutputSpeech()
                {
                    Text = text
                };

                var respattr = new Dictionary<string, object>()
                {
                    { "Education", education},
                    {"Previous", IntentTypes.Intent.GetEmailForInformationRequest},
                };

                return new HandlerResult() { Response = resp, ResponseSessionAttributes = respattr };
            }

            var responseText = $"Do you have a first name? Please give it to me.";

            var innerResponse = new PlainTextOutputSpeech()
            {
                Text = responseText
            };

            if (!string.IsNullOrEmpty(email))
            {
                email = email.Replace(" at ", "@");
            }

            var attr = new Dictionary<string, object>()
            {
                { "Education", education},
                { "Email", email },
                {"Previous", IntentTypes.Intent.GetEmailForInformationRequest },
            };

            return new HandlerResult() { Response = innerResponse, ResponseSessionAttributes = attr };
        }
    }
}