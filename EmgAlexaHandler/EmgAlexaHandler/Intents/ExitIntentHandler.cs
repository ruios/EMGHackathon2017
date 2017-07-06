using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace EmgAlexaHandler.Intents
{
    public class ExitIntentHandler : IIntentHandler
    {
        public bool CanHandle(string name)
        {
            return name == "AMAZON.StopIntent" || name == "AMAZON.CancelIntent";
        }

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            return new HandlerResult
            {
                Response = new PlainTextOutputSpeech
                {
                    Text = $"Thank you for using the EMG search. You will not regret this."
                }
            };
        }
    }
}