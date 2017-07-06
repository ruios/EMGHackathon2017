using System.Collections.Generic;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace EmgAlexaHandler.Intents
{
    public class GetNameForInformationRequestHandler: IIntentHandler
    {
        public bool CanHandle(string name)
        {
            return name == "GetNameForInformationRequest";
        }

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            string responseText;
            var innerResponse = new PlainTextOutputSpeech();

            object education;
            if (session.Attributes["Education"] == null)
            {
                responseText = $"Yeah, no. Say a search word. Pronounce it correctly this time.";

                innerResponse.Text = responseText;

                return new HandlerResult() { Response = innerResponse };
            }
            education = session.Attributes["Education"]; // TODO - CAST TO TYPE

            if (session.Attributes["Email"] == null)
            {
                // TODO - RETURN TO ASK FOR EMAIL
            }

            var email = (string) session.Attributes["Email"];
            var name = intentRequest.Intent.Slots["Name"].Value;

            // TODO - SEND EMAIL TO "INSTITUTE" (FOR DEMO)

            responseText = 
                $"Thank you for the information. We are now sending your request to #institute#. They will contact you shorthly. Do you want to do another search?";

           innerResponse.Text = responseText;
            
            return new HandlerResult() { Response = innerResponse };
        }
    }
}