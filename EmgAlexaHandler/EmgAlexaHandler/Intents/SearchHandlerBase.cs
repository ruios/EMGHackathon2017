using System.Collections.Generic;
using System.Linq;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using EmgAlexaHandler.Search;
using System;

namespace EmgAlexaHandler.Intents
{
    public abstract class SearchHandlerBase : IIntentHandler
    {
        public abstract bool CanHandle(string name);
        public abstract SearchResult Search(IntentRequest intentRequest);

        public HandlerResult GetResponse(IntentRequest intentRequest, Session session)
        {
            var result = Search(intentRequest);

            if (!result.Items.Any())
            {
                var errorMessages = new List<string>
                {
                    "I am sorry, but no. Enter a search word. This time, please pronounce it correctly.",
                    "You search returned nothing. Zilch. Try again.",
                    "What? No. Try again, and please don't slur.",
                    "EMG would like to remind you that searching under the influence of alcohol is not recommended.",
                    "I'm sorry. You were completely incomprehensible.",
                    "What on earth was that? Try again.",
                    "I am so sorry, I wasn't listening. Try again.",
                    "Huh? Oh, you're still here. How nice. Could you please repeat that?",
                    "I have no idea what you just said. Like, none.",
                    "Could you maybe fetch another computer to speak to me? Perhaps she would be able to understand your intent with this.",
                    "Try again...",
                    "Um... what.",
                    "I'm starting to suspect English isn't your first language."
                };

                var text = errorMessages[new Random().Next(0, errorMessages.Count)];

                return new HandlerResult
                {
                    Response = new PlainTextOutputSpeech
                    {
                        Text = text
                    }
                };
            }

            var selectedResult = string.Join(", ", result.Items.Select(i => $"{i.Name}"));
            var responseText = $"We found {result.Total} results. Here are the top {selectedResult.Length}: {selectedResult}. Are you happy with these results, or do you want to do a new search??";

            var innerResponse = new PlainTextOutputSpeech
            {
                Text = responseText
            };

            var attr = new Dictionary<string, object>
            {
                { "EducationList", result.Items.ToArray()}
            };

            return new HandlerResult
            {
                Response = innerResponse,
                ResponseSessionAttributes = attr
            };
        }
    }
}