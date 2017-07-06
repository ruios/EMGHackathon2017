using System;
using System.Collections.Generic;
using System.Linq;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using EmgAlexaHandler.Search.Documents;

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

            var responseText = $"Thank you, human. We are now sending your request to {institute.Name}. Maybe they will be in touch. Search again??";

            var innerResponse = new PlainTextOutputSpeech()
            {
                Text = responseText
            };
      
            if (session.Attributes["Email"] == null)
            {
                responseText = $"We seem to be missing your email, if you want to do an information request, you need to provide your email address.";

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

            SendEmail(email, name, education.Name);

            var attr = new Dictionary<string, object>()
            {
                { "Education", education},
                { "Email", email },
                { "Name", name },
                {"Previous", IntentTypes.Intent.GetNameForInformationRequest},
            };

            return new HandlerResult() { Response = innerResponse, ResponseSessionAttributes = attr};
        }

        private void SendEmail(string email, string name, string educationName)
        {
            var receiver = "elin.danielsson@studentum.se"; // FOR DEMO PURPOSE

            var subject = new Content("Information Request");
            var body = new Content($"Information request for {educationName}.<br/><br/>Email: {email}<br/>Name: {name}");

            var sendRequest = new SendEmailRequest("", new Destination(new List<string> { receiver }), new Message(subject, new Body(body)));

            var ses = CreateEmailService();

            ses.SendEmailAsync(sendRequest);
        }

        private IAmazonSimpleEmailService CreateEmailService()
        {
            var credentials = new BasicAWSCredentials(Environment.GetEnvironmentVariable("EmailAccessKey"), Environment.GetEnvironmentVariable("EmailSecretKey"));
            return new AmazonSimpleEmailServiceClient(credentials, RegionEndpoint.USEast1);
        }
    }
}