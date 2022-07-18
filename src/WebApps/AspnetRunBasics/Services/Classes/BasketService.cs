using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services.Classes
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _client;

        public BasketService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task ChekoutBasket(BasketCheckoutModel model)
        {
            var response = await _client.PostAsJson("/Basket/Checkout", model);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Something a wrong when calling api.");
        }

        public async Task<BasketModel> GetBasket(string userName)
        {
            var response = await _client.GetAsync($"/Basket/{userName}");
            return await response.ReadContentAs<BasketModel>();
        }

        public async Task<BasketModel> UpdateBasket(BasketModel model)
        {
            var response = await _client.PostAsJson("/Basket", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<BasketModel>();

            throw new Exception("Something a wrong when calling api.");
        }
    }
}
