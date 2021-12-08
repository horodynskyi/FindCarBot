using System.Threading.Tasks;
using FindCarBot.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace FindCarBot.Domain.Utils
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

        public static async Task Remove(IDistributedCache cache, PickedParameters model)
        {
            await cache.RemoveAsync(model.Id.ToString());
        }
    }
}