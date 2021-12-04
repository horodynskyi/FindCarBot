namespace FindCarBot.Domain.Services
{
    public class SearchResult
    {
        public string Ids { get; set; }
        
    }

    public class Result
    {
        public SearchResult search_result { get; set; }
    }
}