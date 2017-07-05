using System.Linq;
using Amazon.Lambda.Core;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Alexa.NET.Request.Type;
using EmgAlexaHandler.Intents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace EmgAlexaHandler
{
    public class Function
    {
        private readonly IIntentHandler[] _intentHandlers = new IIntentHandler[0];
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

            var response = new SkillResponse();
            response.Response = new ResponseBody();
            response.Response.ShouldEndSession = false;
            IOutputSpeech innerResponse = null;

            // check what type of a request it is like an IntentRequest or a LaunchRequest
            var requestType = input.GetRequestType();

            if (requestType == typeof(IntentRequest))
            {
                var intentRequest = (IntentRequest)input.Request;

                log.LogLine($"Starting to handle the intent: {intentRequest.Intent.Name}");

                var intent = _intentHandlers.FirstOrDefault(i => i.CanHandle(intentRequest.Intent.Name));

                if (intent != null)
                {
                    innerResponse = intent.GetResponse(intentRequest);
                }
                else
                {
                    //Default
                }
            }
            else if (requestType == typeof(LaunchRequest))
            {
                var responseText = $"Hi, I am EMG bot. What can I help you?";

                innerResponse = new PlainTextOutputSpeech();
                (innerResponse as PlainTextOutputSpeech).Text = responseText;
            }
            else if (requestType == typeof(AudioPlayerRequest))
            {
                // do some audio response stuff
            }

            log.LogLine($"Starting to return the response...");

            response.Response.OutputSpeech = innerResponse;
            response.Version = "1.0";
            return response;
        }
    }
}
