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
            // TODO - get a new fun fact!

            var funFact = $"This is fun!";

            var responseText = $"Here are some fun facts about {education.Name}. {funFact} Do you want to make an information request or hear more about the education?";
            
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