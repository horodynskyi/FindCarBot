using FIndCarBot.Client.Models;

namespace FIndCarBot.Client.DTO
{
    public class ResultCarDTO
    {
        public long USD { get; set; }
        public AutoData AutoData { get; set; }
        public string MarkName { get; set; }
        public string ModelName { get; set; }
        public string LinkToView { get; set; }
        public StateData StateData { get; set; }
        public string PhotoLink { get; set; }
    }
}
