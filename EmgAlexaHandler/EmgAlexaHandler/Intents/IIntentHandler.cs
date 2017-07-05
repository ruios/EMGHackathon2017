using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace EmgAlexaHandler.Intents
{
    public interface IIntentHandler
    {
        bool CanHandle(string name);
        IOutputSpeech GetResponse(IntentRequest intentRequest);
    }
}
