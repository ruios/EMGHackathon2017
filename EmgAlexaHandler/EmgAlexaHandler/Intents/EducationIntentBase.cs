using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using EmgAlexaHandler.Search.Documents;

namespace EmgAlexaHandler.Intents
{
    public abstract class EducationIntentBase : IIntentHandler
    {
        public abstract bool CanHandle(string name);
        public abstract HandlerResult GetResponse(Education education, IntentRequest intentRequest, Session session);

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            

            if (session.Attributes["Education"] == null)
            {
                string responseText = $"Yeah, no. Try again, human. Search for something.";

                var innerResponse = new PlainTextOutputSpeech()
                {
                    Text = responseText
                };

                return new HandlerResult() { Response = innerResponse };
            }

            var education = (Education)session.Attributes["Education"];

            var r = GetResponse(education, intentRequest, session);

            return r;
        }
    }
}
