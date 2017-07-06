using System.Collections.Generic;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using EmgAlexaHandler.Search.Documents;

namespace EmgAlexaHandler.Intents
{
    public class GetNameForInformationRequestHandler: EducationIntentBase
    {
        public override bool CanHandle(string name)
        {
            return name == "GetNameForInformationRequest";
        }

        public override HandlerResult GetResponse(Education education, IntentRequest intentRequest, Session session)
        {
            var responseText = $"Thank you, human. We are now sending your request to #institute#. Maybe they will be in touch. Search again??";

            var innerResponse = new PlainTextOutputSpeech()
            {
                Text = responseText
            };
      
            if (session.Attributes["Email"] == null)
            {
                // TODO - RETURN TO ASK FOR EMAIL
            }

            var email = (string) session.Attributes["Email"];
            var name = intentRequest.Intent.Slots["Name"].Value;

            // TODO - SEND EMAIL TO "INSTITUTE" (FOR DEMO)

            var attr = new Dictionary<string, object>()
            {
                { "Education", education},
                { "Email", email },
                { "Name", name },
            };

            return new HandlerResult() { Response = innerResponse };
        }
    }
}