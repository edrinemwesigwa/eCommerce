using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Infrastructure.Repositotries;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;
    public BasketRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }
  
    public async Task<ShoppingCart?> GetBasket(string userName)
    {
        var basket = await _redisCache.GetStringAsync(userName);
        if (string.IsNullOrEmpty(basket))
            return null;
        return JsonSerializer.Deserialize<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
    {
        await _redisCache.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart));
        return await GetBasket(shoppingCart.UserName) ?? new ShoppingCart();
    }
    public async Task DeleteBasket(string userName)
    {
        await _redisCache.RemoveAsync(userName);
    }

}
