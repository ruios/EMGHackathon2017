using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon;
using Amazon.Lambda.Core;
using Amazon.Runtime;
using Amazon.Runtime.Internal.Util;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using EmgAlexaHandler.Search.Documents;
using Newtonsoft.Json;

namespace EmgAlexaHandler.Intents
{
    public class GetNameForInformationRequestHandler: EducationIntentBase
    {
        public override bool CanHandle(string name)
        {
            return name == "GetNameForInformationRequest";
        }

        public override HandlerResult GetResponse(Education education, IntentRequest intentRequest, Session session)
        {
            var institute = education.Institutes.FirstOrDefault();

            var responseText = $"Thank you. We are now sending your request to {institute.Name}. Maybe they will be in touch. Do another search! I love searching.";

            var innerResponse = new PlainTextOutputSpeech()
            {
                Text = responseText
            };
      
            if (session.Attributes["Email"] == null)
            {
                responseText = $"I'm sorry, but I seem to have dropped your email address somewhere. Please state it again.";

                innerResponse = new PlainTextOutputSpeech()
                {
                    Text = responseText
                };

                var errattr = new Dictionary<string, object>()
                {
                    { "Education", education},
                };

                return new HandlerResult() { Response = innerResponse, ResponseSessionAttributes = errattr };
            }

            var email = (string) session.Attributes["Email"];
            var name = intentRequest.Intent.Slots["Name"].Value;

            LambdaLogger.Log($"Start sending email...");

            SendEmail(email, name, education.Name).Wait();

            var attr = new Dictionary<string, object>()
            {
                { "Education", education},
                { "Email", email },
                { "Name", name },
                {"Previous", IntentTypes.Intent.GetNameForInformationRequest},
            };

            return new HandlerResult() { Response = innerResponse, ResponseSessionAttributes = attr};
        }

        private async Task SendEmail(string email, string name, string educationName)
        {
            var receiver = "elin.danielsson@studentum.se"; // FOR DEMO PURPOSE

            var subject = new Content("Information Request");
            var body = new Content($"Information request for {educationName}.<br/><br/>Email: {email}<br/>Name: {name}");

            var sendRequest = new SendEmailRequest("", new Destination(new List<string> { receiver }), new Message(subject, new Body(body)));

            var ses = CreateEmailService();

            var result = await ses.SendEmailAsync(sendRequest);

            if (result.HttpStatusCode != HttpStatusCode.OK)
            {
                LambdaLogger.Log($"Error sending email...");
                LambdaLogger.Log(JsonConvert.SerializeObject(result));
            }
            else
            {
                LambdaLogger.Log($"Sending email successfully");
            }
        }

        private IAmazonSimpleEmailService CreateEmailService()
        {
            var credentials = new BasicAWSCredentials(Environment.GetEnvironmentVariable("EmailAccessKey"), Environment.GetEnvironmentVariable("EmailSecretKey"));
            return new AmazonSimpleEmailServiceClient(credentials, RegionEndpoint.USEast1);
        }
    }
}