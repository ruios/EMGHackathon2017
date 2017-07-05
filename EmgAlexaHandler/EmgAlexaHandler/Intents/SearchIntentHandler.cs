using System.Collections.Generic;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace EmgAlexaHandler.Intents
{
    public class SearchIntentHandler : IIntentHandler
    {
        public bool CanHandle(string name)
        {
            return name == "SearchIntent";
        }

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            var education = intentRequest.Intent.Slots["Education"].Value;
            var city = intentRequest.Intent.Slots["City"].Value;

            //log.LogLine($"All slots: {string.Join(",", intentRequest.Intent.Slots)}");

            var responseText = $"You are searching for {education} course in {city}";

            var innerResponse = new PlainTextOutputSpeech
            {
                Text = responseText
            };

            
            return new HandlerResult(){Response = innerResponse, ResponseSessionAttributes = new Dictionary<string, object>()};
        }
    }
}