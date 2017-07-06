using System.Collections.Generic;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

namespace EmgAlexaHandler.Intents
{
    public interface IIntentHandler
    {
        bool CanHandle(string name);
        HandlerResult GetResponse(IntentRequest intentRequest, Session session, ILambdaContext context);
    }

    public class HandlerResult
    {
        public IOutputSpeech Response { get; set; }
        public Dictionary<string, object> ResponseSessionAttributes { get; set; }
    }
}
