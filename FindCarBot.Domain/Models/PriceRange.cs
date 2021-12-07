using System;

namespace FindCarBot.Domain.Models
{
    public class PriceRange:BaseModel
    {
        public int StartPrice { get; init; }
        public int EndPrice { get; init; }

        public PriceRange(string startPrice,string endPrice)
        {
            StartPrice = Convert.ToInt32(startPrice);
            EndPrice = Convert.ToInt32(endPrice);
        } 
        public PriceRange()
        {
            
        }
        
    }
}