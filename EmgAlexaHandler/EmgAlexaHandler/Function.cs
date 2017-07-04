using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Alexa.NET.Request.Type;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace EmgAlexaHandler
{
    public class Function
    {
        
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

                switch (intentRequest.Intent.Name)
                {
                    default:
                        var education = intentRequest.Intent.Slots["Education"].Value;
                        var city = intentRequest.Intent.Slots["City"].Value;

                        log.LogLine($"All slots: {string.Join("," , intentRequest.Intent.Slots)}");


                        var responseText = $"You are searching for {education} course in {city}, right?";

                        innerResponse = new PlainTextOutputSpeech();
                        (innerResponse as PlainTextOutputSpeech).Text = responseText;

                        break;
                }
            }
            else if (requestType == typeof(LaunchRequest))
            {
                // default launch path executed
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
