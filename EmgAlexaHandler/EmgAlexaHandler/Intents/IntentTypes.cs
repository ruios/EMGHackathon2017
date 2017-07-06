using System;
using System.Collections.Generic;
using System.Text;

namespace EmgAlexaHandler.Intents
{
    class IntentTypes
    {
        public enum Intent
        {
            None,
            GetEmailForInformationRequest = 1,
            GetMoreInfoAboutEducation = 2,
            GetNameForInformationRequest = 3,
            GoToNewSearch = 4,
            HappyWithSearchResults = 5,
            SelectOneEducation = 6,
            StartInformationRequest = 7
        };
    }
}
