using System.Collections.Generic;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using EmgAlexaHandler.Search.Documents;

namespace EmgAlexaHandler.Intents
{
    public class StartInformationRequestHandler : EducationIntentBase
    {
        public override bool CanHandle(string name)
        {
            return name == "StartInformationRequest";
        }

        public override HandlerResult GetResponse(Education education, IntentRequest intentRequest, Session session)
        {
            var responseText = $"Please give me your email adress. Pronounce it clearly.";

            var innerResponse = new PlainTextOutputSpeech()
            {
                Text = responseText
            };

            var attr = new Dictionary<string, object>()
            {
                { "Education", education},
                {"Previous", IntentTypes.Intent.StartInformationRequest},
            };

            return new HandlerResult() { Response = innerResponse, ResponseSessionAttributes = attr };
        }
    }
}