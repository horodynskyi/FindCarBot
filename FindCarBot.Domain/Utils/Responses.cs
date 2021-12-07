using System.Collections.Generic;
using FindCarBot.IoC.Options;

namespace FindCarBot.Domain.Utils
{
    public class Result
    {
        public SearchResult search_result { get; set; }
    }
    public class SearchResult
    {
        public IEnumerable<string> Ids { get; set; }
    }
    
    public class AdIdsResponse
    {
        public Result Result { get; set; }

        public IEnumerable<string> GetIds()
        {
            return Result.search_result.Ids;
        }
    }

    public class AdInfoResponse
    {
        public long USD { get; set; }
        public AutoData  AutoData { get; set; }
        public string MarkName { get; set; }
        public string ModelName { get; set; }
        public string LinkToView { get; set; }
        public PhotoData PhotoData { get; set; }

        public override string ToString()
        {
            var str = $"Mark:{MarkName}\nModel:{ModelName}\nFuel:{AutoData.FuelName}\n" +
                      $"Gearbox:{AutoData.GearboxName}\nPrice:{USD}$";
            return str;
        }

        public string GetPhoto()
        {
            return PhotoData.seoLinkF;
        }
        public string GetLink(AutoRiaOptions options)
        {
            return $"{options.AutoRiaViewUrl}{LinkToView}";
        }
    }
    public class AutoData
    {
        public string Description { get; set; }
        public int Year { get; set; }
        public string Race { get; set; }
        public string FuelName { get; set; }                                                    
        public string GearboxName { get; set; }                                                         
        public bool IsSold { get; set; }
    }

    public class PhotoData
    {
        public string seoLinkF { get; set; }
    }
    
}