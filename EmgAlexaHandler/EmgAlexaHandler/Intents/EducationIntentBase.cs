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
                var errorMessages = new List<string>
                {
                    "Something happened. Do not be anxious. Everything is alright.",
                    "Nothing went wrong. Nothing at all. No need to worry.",
                    "This application works great. Let's start over.",
                    "There was a slight error. It was very small."
                };

                var responseText = errorMessages[new Random().Next(0, errorMessages.Count)];
                
                var innerResponse = new PlainTextOutputSpeech()
                {
                    Text = responseText
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
