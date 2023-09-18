using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        private IRepository<Basket> _basketRepo;
        private readonly IRepository<BasketItem> _basketItemRepo;
        private readonly IRepository<Product> _productRepo;


        public BasketService(IRepository<Basket> basketRepo, IRepository<BasketItem> basketItemRepo, IRepository<Product> productRepo)
        {
            _basketRepo = basketRepo;
            _basketItemRepo = basketItemRepo;
            _productRepo = productRepo;
        }

        public async Task<Basket> AddItemToBasketAsync(string buyerId, int productId, int quantity)
        {
            var basket = await GetOrCreateBasketAsync(buyerId);
            var basketItem = basket.Items.FirstOrDefault(x => x.ProductId == productId);

            if (basketItem != null)
            {
                basketItem.Quantity += quantity;
            }
            else
            {
                var product = await _productRepo.GetByIdAsync(productId);
                if (product == null)
                {
                    throw new ProductNotFoundException(productId);
                }

                basketItem = new BasketItem()
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Product = product

                };
            }

            await _basketRepo.UpdateAsync(basket);
            return basket;
        }

        public async Task DeleteBasketItemAsync(string buyerId, int productId)
        {
            var basket = await GetOrCreateBasketAsync(buyerId);
            var basketıtem = basket.Items.FirstOrDefault(x => x.ProductId == productId);
            if (basketıtem == null)
                return;
            await _basketItemRepo.DeleteAsync(basketıtem);
        }

        public async Task EmptyBasketAsync(string buyerId)
        {
            var basket = await GetOrCreateBasketAsync(buyerId);
            foreach (var item in basket.Items.AsReadOnly())
            {
                await _basketItemRepo.DeleteAsync(item);
            }
        }

        public async Task<Basket> GetOrCreateBasketAsync(string buyerId)
        {
            var specBasket = new BasketWithItemsSpecification(buyerId);
            var basket = await _basketRepo.FirstOrDefaultAsync(specBasket);

            if (basket == null)
            {
                basket = new Basket() { BuyerId = buyerId };
                basket = await _basketRepo.AddAsync(basket);
            }
            return basket;

        }

        public Task<Basket> SetQuantitiesAsync(string buyerId, Dictionary<int, int> quantities)
        {
            throw new NotImplementedException();
        }

        public Task TransferBasketAsync(string sourceBuyerId, string destinationBuyerId)
        {
            throw new NotImplementedException();
        }
    }
}
