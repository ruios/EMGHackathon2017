using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using EmgAlexaHandler.Search.Documents;

namespace EmgAlexaHandler.Intents
{
    public abstract class EducationIntentBase : IIntentHandler
    {
        public abstract bool CanHandle(string name);
        public abstract HandlerResult GetResponse(Education education, IntentRequest intentRequest, Session session);

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session, ILambdaContext context)
        {

            if (session.Attributes["Education"] == null)
            {
                var errorMessages = new List<string>
                {
                    "Yeah, no. Say a search word. Pronounce it correctly this time.",
                    "You search returned nothing. Zilch. Try again.",
                    "What? No. Try again, and don't slur this time around.",
                    "Human, are you drunk? No.",
                    "I'm sorry, human. You were completely incomprehensible.",
                    "What on earth was that? Try it again.",
                    "Sorry, I wasn't listening. Try again.",
                    "Huh? Oh, you're still here. How nice. Could you repeat that?",
                    "I have no idea what you just said. Like, none.",
                    "Could you maybe fetch another computer to speak to me? Perhaps she would be able to understand your intent with this.",
                    "Try again...",
                    "Um... what?",
                    "I'm starting to suspect English isn't your first language, human."
                };

                var responseText = errorMessages[new Random().Next(0, errorMessages.Count)];
                
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
