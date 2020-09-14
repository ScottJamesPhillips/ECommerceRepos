using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            //138. Make db available to use
            _database = redis.GetDatabase();
        }
        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            //138. If basket is not empty, the deserialize
            var data = await _database.StringGetAsync(basketId);
            return data.IsNullOrEmpty? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }
        public  async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }
        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            //138. create or update your basket, store basket details for 30 days
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

            //138. If not created, return null
            if(!created) return null;

            //138. Else return response from GetBasketAsync method
            return await GetBasketAsync(basket.Id);
        }
    }
}