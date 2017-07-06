using System;
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
    public class SelectOneEducationHandler : IIntentHandler
    {
        public bool CanHandle(string name)
        {
            return name == "SelectOneEducation";
        }

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {

            var errorResponse = new HandlerResult
            {
                Response = new PlainTextOutputSpeech
                {
                    Text = "Oops, let's try another search."
                }
            };

            if (!session.Attributes.ContainsKey("EducationList"))
            {
                return errorResponse;
            }

            var jArray = session.Attributes["EducationList"] as JArray;
            if (jArray != null)
            {
                var educationList = jArray.ToObject<List<Education>>();

                var selected = (string)intentRequest.Intent.Slots["EducationNumber"].Value.ToLower();
            
                Education education = null;
 
                if (selected.Contains("third") || selected.Contains("3") || selected.Contains("three") || selected.Contains("3rd") && educationList.Count >= 3)
                {
                    education = educationList[2];
                }

                else if (selected.Contains("second") || selected.Contains("2") || selected.Contains("two") || selected.Contains("2nd") && educationList.Count >= 2)
                {
                    education = educationList[1];
                }

                else if (selected.Contains("first") || selected.Contains("1") || selected.Contains("one") || selected.Contains("1st") && educationList.Count >= 1)
                {
                    education = educationList[0];
                }
             

                if (education == null)
                {
                    var selectedResult = string.Join(". . . ", educationList.Select(i => $"{i.Name}"));

                    var responseTextWrongSelection = "Which education would you like to choose? The first one, the second one or the third one?";

                    if (educationList.Count == 2)
                    {
                        responseTextWrongSelection = "Which education would you like to choose? The first or the second one?";
                    }
                    else if (educationList.Count == 1)
                    {
                        responseTextWrongSelection = "Do you want to make an information request or hear more about the education?";

                        var innerResponse1 = new PlainTextOutputSpeech
                        {
                            Text = responseTextWrongSelection
                        };
                    }

                    var errorResponseWrongSelection = new PlainTextOutputSpeech
                    {
                        Text = responseTextWrongSelection
                    };

                    var errorResponse2 = new PlainTextOutputSpeech
                    {
                        Text = ErrorMessageHelper.GetErrorMessage()
                    };

                    var errorattr = new Dictionary<string, object>()
                    {
                        { "EducationList", educationList}
                    };

                    return new HandlerResult { Response = errorResponse2, ResponseSessionAttributes = errorattr};
                }

                var responseText = $"Congratulations, you managed to select {education.Name}. Do you want to make an information request or hear more about the education?";
            
                var innerResponse = new PlainTextOutputSpeech
                {
                    Text = responseText
                };

                var attr = new Dictionary<string, object>()
                {
                    {"EducationList", educationList},
                    {"Education", education },
                	{"Previous", IntentTypes.Intent.SelectOneEducation}
				};

                return new HandlerResult() { Response = innerResponse, ResponseSessionAttributes = attr};
            }

            return errorResponse;
        }
    }
}