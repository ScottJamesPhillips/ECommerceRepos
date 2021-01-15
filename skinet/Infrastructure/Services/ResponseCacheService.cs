using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase _database;
        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        // 280
        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            //Method will realize response into JSON format and set response as cache in reddit, with a key and a duration to keep stored
            if(response == null)
            {
                return;
            }
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var serializedResponse = JsonSerializer.Serialize(response, options);
            await _database.StringSetAsync(cacheKey, serializedResponse, timeToLive);
        }

        // 280 
        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            //Method will retrieve any cached information matchin cache key, else will return null
            var cachedResponse = await _database.StringGetAsync(cacheKey);
            if(cachedResponse.IsNullOrEmpty)
            {
                return null;
            }
            return cachedResponse;
        }
    }
}