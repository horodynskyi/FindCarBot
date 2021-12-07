using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace FindCarBot.Domain.Models
{
    public static class CacheModel
    {
        public static async Task SetCache(IDistributedCache cache,PickedParameters model, DistributedCacheEntryOptions options)
        {
            var ser = JsonConvert.SerializeObject(model);
            await cache.SetStringAsync(model.Id.ToString(),ser,options);
        }
        public static async Task<PickedParameters> TryGetCache(IDistributedCache cache, long id)
        {
            var ser = await cache.GetStringAsync(id.ToString());
            if (ser != null)
            {
                var model = JsonConvert.DeserializeObject<PickedParameters>(ser);
                return model;
            }
            return new PickedParameters(){Id = id};
        }
    }
}