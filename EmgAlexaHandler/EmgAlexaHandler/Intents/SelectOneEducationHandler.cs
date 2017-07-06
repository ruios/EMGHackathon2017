using System.Collections.Generic;
using System.Linq;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using EmgAlexaHandler.Search.Documents;

namespace EmgAlexaHandler.Intents
{
    public class SelectOneEducationHandler : IIntentHandler
    {
        public bool CanHandle(string name)
        {
            return name == "SelectOneEducation";
        }

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            if (session.Attributes["EducationList"] == null)
            {
                var errorResponse = new PlainTextOutputSpeech
                {
                    Text = "Oops, let's try another search."
                };

                return new HandlerResult { Response = errorResponse };
            }
            var educationList = (Education[])session.Attributes["EducationList"];

            var selected = (string)intentRequest.Intent.Slots["EducationNumber"].Value.ToLower();
            
            Education education = null;
            if (selected.Contains("third") || selected.Contains("3") || selected.Contains("three"))
            {
                education = educationList[2];
            }

            else if (selected.Contains("second") || selected.Contains("2") || selected.Contains("two"))
            {
                education = educationList[1];
            }

            else if (selected.Contains("first") || selected.Contains("1") || selected.Contains("one"))
            {
                education = educationList[0];
            }

            if (education == null)
            {
                var selectedResult = string.Join(", ", educationList.Select(i => $"{i.Name} from {i.Institutes.First().Name}"));
                var errorResponseText = $"You need to select one of the educations in the list, please do so by picking the first, second or third. Here are the three educations: {selectedResult}. Which one would you like to know more about?";

                var errorResponse = new PlainTextOutputSpeech
                {
                    Text = errorResponseText
                };

                var errorattr = new Dictionary<string, object>()
                {
                    { "EducationList", educationList}
                };

                return new HandlerResult { Response = errorResponse, ResponseSessionAttributes = errorattr};
            }

            // TODO - get a new fun fact!

            var funFact = $"This is fun!";

            var responseText = $"Here are some fun facts about {education.Name}. {funFact} Do you want to make an information request or hear more about the education?";
            
            var innerResponse = new PlainTextOutputSpeech
            {
                Text = responseText
            };

            var attr = new Dictionary<string, object>()
            {
                { "EducationList", educationList},
                {"Education", education }
            };

            return new HandlerResult() { Response = innerResponse, ResponseSessionAttributes = attr};
        }
    }
}