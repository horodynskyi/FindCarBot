using System;

namespace FindCarBot.Domain.Models
{
    public class Dates:BaseModel
    {
        public int StartYears { get; init; }
        public int EndYears { get; init; }

        public Dates(string startYears,string endYears)
        {
            StartYears = Convert.ToInt32(startYears);
            EndYears = Convert.ToInt32(endYears);
        } 
        public Dates()
        {
            
        }
    }
}