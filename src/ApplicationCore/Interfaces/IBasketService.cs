using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> GetOrCreateBasketAsync(string buyerId);

        Task<Basket> AddItemToBasketAsync(string buyerId, int productId, int quantity);

        Task<Basket> SetQuantitiesAsync(string buyerId, Dictionary<int, int> quantities);

        Task DeleteBasketItemAsync(string buyerId, int productId);

        Task EmptyBasketAsync(string buyerId);

        Task TransferBasketAsync(string sourceBuyerId, string destinationBuyerId);
    }
}