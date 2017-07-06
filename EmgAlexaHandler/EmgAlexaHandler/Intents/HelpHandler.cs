using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using EmgAlexaHandler.Search.Documents;
using Nest;
using Newtonsoft.Json.Linq;

namespace EmgAlexaHandler.Intents
{
    public class HelpHandler : IIntentHandler
    {
        public bool CanHandle(string name)
        {
            return name == "AMAZON.HelpIntent";
        }

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            if (session.Attributes["Previous"] == null)
            {
                // DEFAULT HELP!!
            }
            var jObj = session.Attributes["Previous"] as JObject;

            var previous = jObj?.ToObject<IntentTypes.Intent>() ?? IntentTypes.Intent.None;

            Dictionary<string, object> attr = new Dictionary<string, object>();
            var helpText = "Welcome to the help section, human.";
            switch (previous)
            {
                case IntentTypes.Intent.GetEmailForInformationRequest:
                    helpText += "This is where you enter your first name or serial number for the information request you have started on. To continue please state you name.";
                    attr.Add("Education", session.Attributes["Education"]);
                    attr.Add("Email", session.Attributes["Email"]);
                    break;

                case IntentTypes.Intent.GetMoreInfoAboutEducation:
                    helpText += "This is where you enter your first name or serial number for the information request you have started on. To continue please state you name.";
                    attr.Add("Education", session.Attributes["Education"]);
                    attr.Add("Email", session.Attributes["Email"]);
                    break;

                default:
                    helpText = "this is the default help!!!";
                        break;
            }

            helpText += "If you want to exit, you can say";


            return new HandlerResult()
            {
                Response = new PlainTextOutputSpeech() { Text = helpText},ResponseSessionAttributes = attr
            };
        }
    }
}
