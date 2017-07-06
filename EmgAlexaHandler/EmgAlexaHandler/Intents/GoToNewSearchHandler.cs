using System.Collections.Generic;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

namespace EmgAlexaHandler.Intents
{
    public class GoToNewSearchHandler : IIntentHandler
    {
        public bool CanHandle(string name)
        {
            return name == "GoToNewSearch";
        }

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            var responseText = $"What would you like to search for?";

            var innerResponse = new PlainTextOutputSpeech
            {
                Text = responseText
            };

            return new HandlerResult() { Response = innerResponse };
        }
    }
}