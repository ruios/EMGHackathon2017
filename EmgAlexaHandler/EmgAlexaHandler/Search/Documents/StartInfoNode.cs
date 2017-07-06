using System;
using Nest;

namespace EmgAlexaHandler.Search.Documents
{
    public class StartInfoNode
    {
        [Date(Name = "convertedStartDate")]
        public DateTime? StartDate { get; set; }
    }
}