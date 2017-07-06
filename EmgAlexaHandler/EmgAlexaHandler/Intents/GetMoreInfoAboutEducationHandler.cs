using System;
using System.Collections.Generic;
using System.Linq;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using EmgAlexaHandler.Search.Documents;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EmgAlexaHandler.Intents
{
    public class GetMoreInfoAboutEducationHandler : EducationIntentBase
    {
        public override bool CanHandle(string name)
        {
            return name == "GetMoreInfoAboutEducation";
        }

        public override HandlerResult GetResponse(Education education, IntentRequest intentRequest, Session session)
        {
            List<string> funFacts;
            if (!session.Attributes.ContainsKey("FunFacts"))
            {
                funFacts = GetAllFunFacts(education);
            }
            else
            {
                var jArray = session.Attributes["FunFacts"] as JArray;
                funFacts = jArray?.ToObject<List<string>>() ?? new List<string>();
            }

            if (!funFacts.Any())
            {
                var text = $"I'm so sorry, there are no more fun facts. Nothing is fun anymore. Would you like to do an information request or try a new search?";

                var resp = new PlainTextOutputSpeech()
                {
                    Text = text
                };

                var respattr = new Dictionary<string, object>()
                {
                    { "Education", education},
                    {"FunFacts", funFacts },
                    {"Previous", IntentTypes.Intent.GetMoreInfoAboutEducation}
                };

                return new HandlerResult() { Response = resp, ResponseSessionAttributes = respattr};
            }

            var funFact = funFacts.OrderBy(f => Guid.NewGuid()).First();

            LambdaLogger.Log($"Fun fact selected: {funFact}");
            LambdaLogger.Log(JsonConvert.SerializeObject(funFacts));

            funFacts.Remove(funFact);

            var responseText = $"Here are some questionably fun facts about {education.Name}. {funFact} Do you want to make an information request or hear more about the education?";
            
            var innerResponse = new PlainTextOutputSpeech()
            {
                Text = responseText
            };

            var attr = new Dictionary<string, object>()
            {
                { "Education", education},
				{"FunFacts", funFacts },
                {"Previous", IntentTypes.Intent.GetMoreInfoAboutEducation},
            };

            return new HandlerResult(){Response = innerResponse, ResponseSessionAttributes = attr};
        }

        private List<string> GetAllFunFacts(Education education)
        {
            var facts = new List<string>();

            var review = education.Fields.ReviewCount.HasValue
                ? $"This education has {education.Fields.ReviewCount} reviews, with an average of {education.Fields.ReviewAverage}. {(education.Fields.ReviewAverage > 3 ? "That's pretty good" : "" )}"
                : $"This education doesn't have any reviews yet";
            facts.Add(review);

            var category = $"The categories are {string.Join(", ", education.Categories.Select(c => c.Name))}. That sounds interesting.";
            facts.Add(category);

            if (education.Events.Any())
            {
                var locations = education.Events.Select(e => e.Location.Name).Distinct().ToArray();
                var location = locations.Count() >= 3
                    ? $"The education has events in a lot of places, here are some: {string.Join(", ", locations.Take(3))}"
                    : $"The education has events in {string.Join(", ", locations)}";
                facts.Add(location);

                var nextEvent = education.Events.Where(e => e.Start.StartDate.HasValue).OrderByDescending(e => e.Start.StartDate).FirstOrDefault();
                var @event = nextEvent != null ? $"The next event will take place on {nextEvent.Start.StartDate:D}, in {nextEvent.Location.Name}" : $"This education doesn't have any events.";
                facts.Add(@event);
            }

            return facts;
        }
    }
}