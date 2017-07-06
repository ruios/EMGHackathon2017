using System.Collections.Generic;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using EmgAlexaHandler.Search.Documents;

namespace EmgAlexaHandler.Intents
{
    public class GetMoreInfoAboutEducationHandler : EducationIntentBase
    {
        public override bool CanHandle(string name)
        {
            return name == "GetMoreInfoAboutEducation";
        }

        public override HandlerResult GetResponse(Education education, IntentRequest intentRequest, Session session)
        {
            var responseText = $"Here are some facts about the education you chose: "; // TODO - GET FACTS FROM EDUCATION

            var innerResponse = new PlainTextOutputSpeech()
            {
                Text = responseText
            };

            var attr = new Dictionary<string, object>()
            {
                { "Education", education}
            };

            return new HandlerResult(){Response = innerResponse, ResponseSessionAttributes = attr};
        }
    }
}