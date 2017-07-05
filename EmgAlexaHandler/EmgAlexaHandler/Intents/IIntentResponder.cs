using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace EmgAlexaHandler.Intents
{
    public interface IIntentResponder
    {
        bool CanRespond(string name);
        IOutputSpeech GetResponse(IntentRequest intentRequest);
    }

    public class SearchIntentResponder : IIntentResponder
    {
        public bool CanRespond(string name)
        {
            return name == "SearchIntent";
        }

        public IOutputSpeech GetResponse(IntentRequest intentRequest)
        {
            var education = intentRequest.Intent.Slots["Education"].Value;
            var city = intentRequest.Intent.Slots["City"].Value;

            //log.LogLine($"All slots: {string.Join(",", intentRequest.Intent.Slots)}");

            var responseText = $"You are searching for {education} course in {city}";

            var innerResponse = new PlainTextOutputSpeech
            {
                Text = responseText
            };

            return innerResponse;
        }
    }
}
