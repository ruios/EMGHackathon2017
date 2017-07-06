using System.Collections.Generic;
using System.Linq;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using EmgAlexaHandler.Search;

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
                return new HandlerResult
                {
                    Response = new PlainTextOutputSpeech
                    {
                        Text = "Your search was frankly not very good. Please try again."
                    }
                };
            }

            var selectedResult = string.Join(", ", result.Items.Select(i => $"{i.Name}"));
            var responseText = $"We found {result.Total} results. Here are the top three: {selectedResult}. Are you happy with these results, or do you want to do a new search??";

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