using System.Collections.Generic;
using System.Linq;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using EmgAlexaHandler.Search.Documents;
using Newtonsoft.Json.Linq;

namespace EmgAlexaHandler.Intents
{
    public class HappyWithSearchResultsHandler : IIntentHandler
    {
        public bool CanHandle(string name)
        {
            return name == "HappyWithSearchResults";
        }

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            var errorResponse = new HandlerResult
            {
                Response = new PlainTextOutputSpeech
                {
                    Text = ErrorMessageHelper.GetErrorMessage()
                }
            };

            if (session.Attributes == null || !session.Attributes.ContainsKey("EducationList"))
            {
                return errorResponse;
            }

            var jArray = session.Attributes["EducationList"] as JArray;
            if (jArray != null)
            {
                var educationList = jArray.ToObject<List<Education>>();

                var selectedResult = string.Join(". . . ", educationList.Select(i => $"{i.Name}"));

                string responseText = "Which education would you like to choose? The first one, the second one or the third one?";

                if (educationList.Count == 2)
                {
                    responseText = "Which education would you like to choose? The first or the second one?";
                }
                else if (educationList.Count == 1)
                {
                    responseText = "Do you want to make an information request or hear more about the education?";

                    var innerResponse1 = new PlainTextOutputSpeech
                    {
                        Text = responseText
                    };

                    var attr1 = new Dictionary<string, object>()
                    {
                        { "EducationList", educationList},
                        {"Education", educationList.First() },
                        {"Previous", IntentTypes.Intent.HappyWithSearchResults},
                    };
                    return new HandlerResult() { Response = innerResponse1, ResponseSessionAttributes = attr1 };

                }

                var innerResponse = new PlainTextOutputSpeech
                {
                    Text = responseText
                };

                var attr = new Dictionary<string, object>()
                {
                    { "EducationList", educationList},
                    {"Previous", IntentTypes.Intent.HappyWithSearchResults},
                };

                return new HandlerResult() { Response = innerResponse, ResponseSessionAttributes = attr};
            }

            return errorResponse;
        }
    }
}