using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using EmgAlexaHandler.Search.Documents;
using Newtonsoft.Json.Linq;

namespace EmgAlexaHandler.Intents
{
    public abstract class EducationIntentBase : IIntentHandler
    {
        public abstract bool CanHandle(string name);
        public abstract HandlerResult GetResponse(Education education, IntentRequest intentRequest, Session session);

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            if (!session.Attributes.ContainsKey("Education"))
            {
                var innerResponse = new PlainTextOutputSpeech()
                {
                    Text = ErrorMessageHelper.GetErrorMessage()
                };

                return new HandlerResult() { Response = innerResponse };
            }

            var jObject = session.Attributes["Education"] as JObject;
            if (jObject != null)
            {
                var education = jObject.ToObject<Education>();

                var r = GetResponse(education, intentRequest, session);

                return r;
            }

            return new HandlerResult
            {
                Response = new PlainTextOutputSpeech
                {
                    Text = "Oops, something went wrong. Please try saying it with more optimism."
                }
            };
        }
    }
}
