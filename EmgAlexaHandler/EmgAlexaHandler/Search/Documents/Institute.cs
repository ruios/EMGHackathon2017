﻿using Nest;

namespace EmgAlexaHandler.Search.Documents
{
    public class Institute
    {
        [Text(Name = "name")]
        public string Name { get; set; }
    }
}