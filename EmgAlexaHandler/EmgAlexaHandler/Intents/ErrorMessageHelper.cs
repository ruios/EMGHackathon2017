using System;
using System.Collections.Generic;
using System.Text;

namespace EmgAlexaHandler.Intents
{
    public static class ErrorMessageHelper
    {
        public static string GetErrorMessage()
        {
            var errorMessages = new List<string>
            {
                "Something happened. Do not be anxious. Everything is alright.",
                "Nothing went wrong. Nothing at all. No need to worry.",
                "This application works great. Let's start over.",
                "There was a slight error. It was very small."
            };

            return errorMessages[new Random().Next(0, errorMessages.Count)];
        }
    }
}
