﻿using System.Collections.Generic;
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
            var responseText = $"Let's search again, because it's so much fun. What would you like to search for?";

            var innerResponse = new PlainTextOutputSpeech
            {
                Text = responseText
            };

            var attr = new Dictionary<string, object>()
            {
                {"Previous", IntentTypes.Intent.GoToNewSearch},
            };

            return new HandlerResult() { Response = innerResponse, ResponseSessionAttributes = attr};

        }
    }
}