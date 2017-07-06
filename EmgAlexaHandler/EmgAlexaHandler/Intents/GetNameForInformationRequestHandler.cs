using System.Collections.Generic;
using System.Linq;
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
            var institute = education.Institutes.FirstOrDefault();

            var responseText = $"Thank you, human. We are now sending your request to {institute.Name}. Maybe they will be in touch. Search again??";

            var innerResponse = new PlainTextOutputSpeech()
            {
                Text = responseText
            };
      
            if (session.Attributes["Email"] == null)
            {
                responseText = $"We seem to be missing your email, if you want to do an information request, you need to provide your email address.";

                innerResponse = new PlainTextOutputSpeech()
                {
                    Text = responseText
                };

                var errattr = new Dictionary<string, object>()
                {
                    { "Education", education}
                };

                return new HandlerResult() { Response = innerResponse, ResponseSessionAttributes = errattr };
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