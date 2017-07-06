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
                    helpText += "This is where you enter your first name or bar code for the information request you have started on. To continue please state you name.";
                    attr.Add("Education", session.Attributes["Education"]);
                    attr.Add("Email", session.Attributes["Email"]);
                    break;

                case IntentTypes.Intent.GetMoreInfoAboutEducation:
                    helpText += "You really only have two options here. How hard can it be?";
                    attr.Add("Education", session.Attributes["Education"]);
                    break;

                case IntentTypes.Intent.GetNameForInformationRequest:
                    helpText += "Do a new search. Scramble your brain for a search word, or any word. Really, any verbalized sign of intelligence will do at this point.";
                    attr.Add("Education", session.Attributes["Education"]);
                    attr.Add("Email", session.Attributes["Email"]);
                    attr.Add("Name", session.Attributes["Name"]);
                    break;

                case IntentTypes.Intent.GoToNewSearch:
                    helpText += "Do a new search. Scramble your brain for a search word, or any word. Really, any verbalized sign of intelligence will do at this point.";
                    break;

                case IntentTypes.Intent.HappyWithSearchResults:
                    helpText += "You got two choices. Now choose. Information request or more information? Life is short.";
                    attr.Add("EducationList", session.Attributes["EducationList"]);
                    break;

                case IntentTypes.Intent.SelectOneEducation:
                    helpText += "You got three to choose from. Now choose. Say 'first', 'second' or 'third'. Do it.";
                    attr.Add("EducationList", session.Attributes["EducationList"]);
                    break;

                case IntentTypes.Intent.StartInformationRequest:
                    helpText += "You got three to choose from. Now choose. Say 'first', 'second' or 'third'. Do it.";
                    attr.Add("EducationList", session.Attributes["EducationList"]);
                    break;


                default:
                    helpText = "There is really nothing special to add here.";
                        break;
            }

            helpText += "If you want to start over, just say 'stop'.";


            return new HandlerResult()
            {
                Response = new PlainTextOutputSpeech() { Text = helpText},ResponseSessionAttributes = attr
            };
        }
    }
}
