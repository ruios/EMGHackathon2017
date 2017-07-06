using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.Core;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Alexa.NET.Request.Type;
using EmgAlexaHandler.Intents;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace EmgAlexaHandler
{
    public class Function
    {
        private readonly IIntentHandler[] _intentHandlers = new IIntentHandler[]
            {
                new GetMoreInfoAboutEducationHandler(),
                new SearchIntentHandler(),
                new StartInformationRequestHandler(), 
                new GetEmailForInformationRequestHandler(), 
                new GetNameForInformationRequestHandler(), 
                new GoToNewSearchHandler(),
                new HappyWithSearchResultsHandler(), 
                new SelectOneEducationHandler(), 
                
            };

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            var log = context.Logger;
            log.LogLine($"Starting to handle the request...");

            log.LogLine(JsonConvert.SerializeObject(input));

            var response = new SkillResponse();
            response.Response = new ResponseBody();
            response.Response.ShouldEndSession = false;
            IOutputSpeech innerResponse = null;

            var sessionAttributes = new Dictionary<string, object>();

            // check what type of a request it is like an IntentRequest or a LaunchRequest
            var requestType = input.GetRequestType();

            if (requestType == typeof(IntentRequest))
            {
                var intentRequest = (IntentRequest)input.Request;

                log.LogLine($"Starting to handle the intent: {intentRequest.Intent.Name}");
                
                var intent = _intentHandlers.FirstOrDefault(i => i.CanHandle(intentRequest.Intent.Name));

                if (intent != null)
                {
                    var resp = intent.GetResponse(intentRequest, input.Session, context);
                    innerResponse = resp.Response;
                    sessionAttributes = resp.ResponseSessionAttributes;
                }
                else
                {
                    //Default
                    log.LogLine("Intent not found");
                }
            }
            else if (requestType == typeof(LaunchRequest))
            {
                var responseText = $"Welcome to the education search, human! What would you like to search for?";

                innerResponse = new PlainTextOutputSpeech();

                (innerResponse as PlainTextOutputSpeech).Text = responseText;
            }
            else if (requestType == typeof(AudioPlayerRequest))
            {
                // do some audio response stuff
            }

            log.LogLine($"Starting to return the response...");
            
            response.SessionAttributes = sessionAttributes;
            
            response.Response.OutputSpeech = innerResponse;
            response.Version = "1.0";

            log.Log(JsonConvert.SerializeObject(response));

            return response;
        }
    }
}
