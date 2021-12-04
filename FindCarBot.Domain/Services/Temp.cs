using System.Collections.Generic;

namespace FindCarBot.Domain.Services
{

    public class SearchResult
    {
        public IEnumerable<string> Ids { get; set; }

    }

    public class Result
    {
        public SearchResult search_result { get; set; }
    }

    public class Response
    {
        public Result Result { get; set; }
    }
}